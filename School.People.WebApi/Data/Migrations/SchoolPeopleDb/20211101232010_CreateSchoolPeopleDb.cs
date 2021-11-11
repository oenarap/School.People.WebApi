using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.People.WebApi.Migrations.SchoolPeopleDb
{
    public partial class CreateSchoolPeopleDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressIdsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResidentialAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PermanentAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommunityTaxCertificateIssuanceAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressIdsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyMemberDetailsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgencyId = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    GsisIdNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PagIbigIdNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PhilhealthNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    SssNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Tin = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyMemberDetailsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterReferencesIdsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceA = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferenceB = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferenceC = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterReferencesIdsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CitizenshipsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DualCitizenshipMode = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    DualCitizenship = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenshipsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CivicWorksTable",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalHoursWorked = table.Column<double>(type: "float", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsOngoing = table.Column<bool>(type: "bit", nullable: false),
                    LocationAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PositionTitle = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EmployerOrganizationOrBusinessName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CivicWorksTable", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateOfBirthsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Birthdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateOfBirthsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationsTable",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DegreeCourse = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IfGraduatedYearGraduated = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    IfNotGraduatedHighestLevelOrUnitsEarned = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ScholarshipOrHonorsReceived = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    IsOngoing = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationsTable", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "EligibilitiesTable",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EligibilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    DateOfExamOrConferment = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PlaceOfExamConferment = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LicenseNumberIfApplicable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseDateOfRelease = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EligibilitiesTable", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "FamilyIdsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MotherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FatherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyIdsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaqsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRelatedToAuthorityThirdDegree = table.Column<bool>(type: "bit", nullable: false),
                    IsRelatedToAuthorityFourthDegree = table.Column<bool>(type: "bit", nullable: false),
                    RelationshipToAuthorityDetails = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsGuiltyOfAdministrativeOffense = table.Column<bool>(type: "bit", nullable: false),
                    AdministrativeOffenseDetails = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    WasCriminallyCharged = table.Column<bool>(type: "bit", nullable: false),
                    CriminalChargesFilingDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CriminalChargesCaseStatus = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    WasConvicted = table.Column<bool>(type: "bit", nullable: false),
                    ConvictionDetails = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    WasSeparatedFromService = table.Column<bool>(type: "bit", nullable: false),
                    SeparationFromServiceDetails = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    WasNatlOrLocalElectionCandidate = table.Column<bool>(type: "bit", nullable: false),
                    NatlOrLocalElectionCandidacyDetails = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    HasResignedForCandidacy = table.Column<bool>(type: "bit", nullable: false),
                    ResignationForCandidacyDetails = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    HasAcquiredImmigrantStatus = table.Column<bool>(type: "bit", nullable: false),
                    OriginCountry = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsIndigenousGroupMember = table.Column<bool>(type: "bit", nullable: false),
                    IndigenousGroupMembershipDetails = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDifferentlyAbled = table.Column<bool>(type: "bit", nullable: false),
                    DifferentlyAbledIdNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    IsSoloParent = table.Column<bool>(type: "bit", nullable: false),
                    SoloParentIdNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImagesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecentPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtherInformationsTable",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    DescriptiveName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherInformationsTable", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "PeopleTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    NameExtension = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    IsPersonnel = table.Column<bool>(type: "bit", nullable: false),
                    IsStudent = table.Column<bool>(type: "bit", nullable: false),
                    IsOther = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonDetailsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    CivilStatus = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    OtherCivilStatus = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    HeightInCentimeters = table.Column<double>(type: "float", nullable: false),
                    WeightInKilograms = table.Column<double>(type: "float", nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDetailsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingsTable",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleOfTrainingProgram = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DurationHours = table.Column<double>(type: "float", nullable: false),
                    Sponsor = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsOngoing = table.Column<bool>(type: "bit", nullable: false),
                    LocationAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingsTable", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "VerificationDetailsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommunityTaxCertificateNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    CommunityTaxCertificateIssuanceDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationDetailsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorksTable",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonthlySalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalaryGradeAndStepIncrement = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    StatusOfAppointment = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    IsGovernmentService = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsOngoing = table.Column<bool>(type: "bit", nullable: false),
                    LocationAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PositionTitle = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EmployerOrganizationOrBusinessName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorksTable", x => x.Index);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressIdsTable");

            migrationBuilder.DropTable(
                name: "AgencyMemberDetailsTable");

            migrationBuilder.DropTable(
                name: "CharacterReferencesIdsTable");

            migrationBuilder.DropTable(
                name: "CitizenshipsTable");

            migrationBuilder.DropTable(
                name: "CivicWorksTable");

            migrationBuilder.DropTable(
                name: "ContactDetailsTable");

            migrationBuilder.DropTable(
                name: "DateOfBirthsTable");

            migrationBuilder.DropTable(
                name: "EducationsTable");

            migrationBuilder.DropTable(
                name: "EligibilitiesTable");

            migrationBuilder.DropTable(
                name: "FamilyIdsTable");

            migrationBuilder.DropTable(
                name: "FaqsTable");

            migrationBuilder.DropTable(
                name: "ImagesTable");

            migrationBuilder.DropTable(
                name: "OtherInformationsTable");

            migrationBuilder.DropTable(
                name: "PeopleTable");

            migrationBuilder.DropTable(
                name: "PersonDetailsTable");

            migrationBuilder.DropTable(
                name: "TrainingsTable");

            migrationBuilder.DropTable(
                name: "VerificationDetailsTable");

            migrationBuilder.DropTable(
                name: "WorksTable");
        }
    }
}
