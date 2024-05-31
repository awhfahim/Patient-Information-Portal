using Autofac.Extras.Moq;
using Mapster;
using Moq;
using PatientPortal.Application.Contracts.DTOs;
using PatientPortal.Application.Contracts.Utilities;
using PatientPortal.Application.Services;
using PatientPortal.Domain.PatientAggregate;
using PatientPortal.Domain.Primitives;
using PatientPortal.Domain.Repositories;
using Shouldly;
using Epilepsy = PatientPortal.Domain.PatientAggregate.Epilepsy;

namespace PatientPortal.Application.Tests;

public class PatientManagementServiceTests
{
    private AutoMock _mock;
    private Mock<IApplicationUnitOfWork> _unitOfWork;
    private Mock<IGuidProvider> _guidProvider;
    private Mock<IPatientRepository> _patientRepository;
    private PatientManagementService _patientManagementService;
    
    [SetUp]
    public void Setup()
    {
        TypeAdapterConfig<PatientCreateDto, Patient>
            .NewConfig()
            .MapWith(src => new Patient
            (
                src.Name,
                src.Age,
                src.Gender,
                src.BloodGroup,
                src.DiseaseInfoId,
                (Epilepsy)src.EpilepsyStatus,
                src.Address,
                src.PhoneNumber
            ));
        
        _unitOfWork = _mock.Mock<IApplicationUnitOfWork>();
        _guidProvider = _mock.Mock<IGuidProvider>();
        _patientRepository = _mock.Mock<IPatientRepository>();
        _patientManagementService = _mock.Create<PatientManagementService>();
    }
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _mock = AutoMock.GetLoose();
    }

    [TearDown]
    public void TearDown()
    {
        _unitOfWork.Reset();
        _guidProvider.Reset();
        _patientRepository.Reset();
        
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _mock.Dispose();
    }

    [Test]
    public async Task AddPatientAsync_WhenCalled_ReturnsGuid()
    {
        // Arrange
        
        var diseaseId = Guid.NewGuid();
        
        var patientCreateDto = new PatientCreateDto("John",23,"Male","A+", 2,
            diseaseId, new List<string>()
            {
                "1B4F84FE-D1E7-4D7E-A985-35DFE950CBFD", "EF4EBD97-BEAF-4219-BE88-09CBE2863607" 
            }, new List<string>()
            {
                "9109A751-5B70-40F9-BE17-4668577CCCA9", "E6CC0251-9030-41C9-96D6-A57C21B25B42" 
                
            }, "01581899069", "Mirpur 2");
        
        var cancellationToken = new CancellationToken();
        
        _guidProvider.Setup(x => x.GetGuid()).Returns(Guid.NewGuid()).Verifiable();
        
        _unitOfWork.Setup(x 
            => x.PatientRepository.AddAsync(It.IsAny<Patient>(), cancellationToken))
            .Returns(Task.CompletedTask).Verifiable();
        
        _unitOfWork.Setup(x => x.SaveAsync())
            .Returns(Task.CompletedTask).Verifiable();
        
        //Act
        var result = await _patientManagementService.AddPatientAsync(patientCreateDto, cancellationToken);
        
        // Assert
        this.ShouldSatisfyAllConditions(
            () => _guidProvider.VerifyAll(),
            () => _unitOfWork.VerifyAll(),
            () => result.IsError.ShouldBeFalse(),
            () => result.Value.ShouldNotBe(Guid.Empty)
        );
    }

    [Test]
    public async Task GetPatientsAsync_WhenCalled_ReturnsListOfGetPatientDto()
    {
        // Arrange
        var cancellationToken = new CancellationToken();

        var patients = new List<PatientData>
        {
            new (){Id = Guid.NewGuid(), Name = "John",Gender = "Male",PhoneNumber = "082343234",Address = "Dhaka",DiseaseName = "Covid",BloodGroup = "A+",Age = 23,EpilepsyStatus = 1},
            new (){Id = Guid.NewGuid(), Name = "John",Gender = "Male",PhoneNumber = "082343234",Address = "Dhaka",DiseaseName = "Covid",BloodGroup = "A+",Age = 23,EpilepsyStatus = 1},
            new (){Id = Guid.NewGuid(), Name = "John",Gender = "Male",PhoneNumber = "082343234",Address = "Dhaka",DiseaseName = "Covid",BloodGroup = "A+",Age = 23,EpilepsyStatus = 1}
        };
        
        _unitOfWork.Setup(x => x.PatientRepository.GetAllPatientsAsync(cancellationToken))
            .ReturnsAsync(patients).Verifiable();
        
        // Act
        var result = await _patientManagementService.GetPatientsAsync(cancellationToken);
        
        // Assert
        this.ShouldSatisfyAllConditions(
            () => _unitOfWork.VerifyAll(),
            () => result.IsError.ShouldBeFalse(),
            () => result.Value.Count.ShouldBe(3)
        );
    }

    [Test]
    public async Task GetPatientAsync_WhenCalled_ReturnsGetPatientByIdDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cancellationToken = new CancellationToken();
        
        var patient = new PatientData
        {
            Id = Guid.NewGuid(), Name = "John",Gender = "Male",PhoneNumber = "082343234",Address = "Dhaka",DiseaseName = "Covid",BloodGroup = "A+",Age = 23,EpilepsyStatus = 1
        };
        
        var allergies = new List<string> {"Allergy1", "Allergy2"};
        var ncds = new List<string> {"Ncd1", "Ncd2"};
        
        _unitOfWork.Setup(x => x.PatientRepository.MyGetByIdAsync(id, cancellationToken))
            .ReturnsAsync((patient, allergies, ncds)).Verifiable();
        
        // Act
        var result = await _patientManagementService.GetPatientAsync(id, cancellationToken);
        
        // Assert
        this.ShouldSatisfyAllConditions(
            () => _unitOfWork.VerifyAll(),
            () => result.IsError.ShouldBeFalse(),
            () => result.Value.Id.ShouldBe(patient.Id),
            () => result.Value.Name.ShouldBe(patient.Name),
            () => result.Value.AllergiesDetails.Count.ShouldBe(2),
            () => result.Value.NcdDetails.Count.ShouldBe(2));
    }
    
    [Test]
    public async Task DeletePatientAsync_WhenCalled_ReturnsGuid()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cancellationToken = new CancellationToken();
        
        _unitOfWork.Setup(x => x.PatientRepository.RemoveAsync(id, cancellationToken))
            .Returns(Task.CompletedTask).Verifiable();
        
        _unitOfWork.Setup(x => x.SaveAsync())
            .Returns(Task.CompletedTask).Verifiable();
        
        // Act
        var result = await _patientManagementService.DeletePatientAsync(id, cancellationToken);
        
        // Assert
        this.ShouldSatisfyAllConditions(
            () => _unitOfWork.VerifyAll(),
            () => result.IsError.ShouldBeFalse(),
            () => result.Value.ShouldBe(id)
        );
    }

    [Test]
    public async Task UpdatePatientAsync_WhenCalled_ReturnsGuid()
    {
        // Arrange
        var token = new CancellationToken();
        var patient = new Patient("Robin", 23, "Male", "O-", Guid.NewGuid(), Epilepsy.Yes, "Faridpur", "0158484259");
        var patientUpdateDto = new PatientUpdateDto(Guid.NewGuid(), "Fahim", 24, "Male", "O+", 2, Guid.NewGuid(), "Mirpur 2", "01581899069");
        _unitOfWork.Setup(x => x.PatientRepository.GetByIdAsync(It.IsAny<Guid>(), token))
            .ReturnsAsync(patient).Verifiable();
        
        _unitOfWork.Setup(x => x.SaveAsync())
            .Returns(Task.CompletedTask).Verifiable();
        
        // Act
        var result = await _patientManagementService.UpdatePatientAsync(patientUpdateDto, token);
        
        //Assert
        this.ShouldSatisfyAllConditions(
            () => _unitOfWork.VerifyAll(),
            () => result.IsError.ShouldBeFalse(),
            () => result.Value.ShouldBe(patient.Id)
        );
    }
}