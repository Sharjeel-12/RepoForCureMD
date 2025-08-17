namespace PatientVisitManager.Api.DTOs;
public record CreatePatientDto(string PatientName, string PatientEmail, string PatientPhone, string PatientDescription, int? VisitID);
public record UpdatePatientDto(int PatientID, string PatientName, string PatientEmail, string PatientPhone, string PatientDescription, int? VisitID);
