using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using nic_api.DataAccess;
using nic_api.Domain;
using nic_api.Extensions;
using nic_api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssemblyContaining<CreateDoctorValidator>(ServiceLifetime.Transient); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDb>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("mssql")
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDb>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    var area1 = new Area { Number = "Участок 1" };
    var area2 = new Area { Number = "Участок 2" };
    var office1 = new Office { Number = "Кабинет 1" };
    var office2 = new Office { Number = "Кабинет 2" };
    var specialization1 = new Specialization { Name = "Терапевт" };
    var specialization2 = new Specialization { Name = "Психолог" };
    var doctor1 = new Doctor
    {
        Fio = "Иванов И.И.",
        Area = area1,
        Office = office1,
        Specialization = specialization1
    };
    var doctor2 = new Doctor
    {
        Fio = "Петров П.П.",
        Office = office2,
        Specialization = specialization2
    };
    var patient1 = new Patient
    {
        FirstName = "Сидор",
        MiddleName = "Сидорович",
        LastName = "Сидоров",
        Address = "Брайтон Бич 18",
        BirthDate = DateTime.Parse("1921-05-28"),
        Sex = Sex.Male,
        Area = area1
    };
    db.Doctors.AddRange(doctor1, doctor2);
    db.Patients.AddRange(patient1);
    db.SaveChanges();
}

# region Doctors api
app.MapGet("/api/doctors", async (int page, int pageSize, string sortField, string sortOrder, IMapper mapper, AppDb db) => await db.Doctors    
    .Include(d => d.Area)
    .Include(d => d.Office)
    .Include(d => d.Specialization)
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .OrderByDynamic(sortField, sortOrder)
    .Select(d => mapper.Map<IndexDoctor>(d))
    .AsNoTracking()
    .ToListAsync())
    .WithTags("Doctors");

app.MapGet("/api/doctors/{id:int}", async (int id, IMapper mapper, AppDb db) =>
    {
        var doctor = await db.Doctors.FindAsync(id);
        if (doctor == null) return Results.NotFound();
        return Results.Ok(mapper.Map<UpdateDoctor>(doctor));
    })
    .Produces<UpdateDoctor>()
    .Produces(StatusCodes.Status404NotFound)
    .WithTags("Doctors");

app.MapPost("/api/doctors", async (CreateDoctor doctorData, IValidator<CreateDoctor> validator, IMapper mapper, AppDb db) =>
    {
        var result = await validator.ValidateAsync(doctorData);
        if (!result.IsValid) return Results.ValidationProblem(result.ToDictionary());
        var doctor = mapper.Map<Doctor>(doctorData);
        await db.Doctors.AddAsync(doctor);
        await db.SaveChangesAsync();
        return Results.Created($"/api/doctors/{doctor.Id}", mapper.Map<UpdateDoctor>(doctor));
    })
    .Produces<UpdateDoctor>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .WithTags("Doctors");

app.MapPut("/api/doctors", async (UpdateDoctor doctorData, IValidator<UpdateDoctor> validator, IMapper mapper, AppDb db) =>
    {
        var result = await validator.ValidateAsync(doctorData);
        if (!result.IsValid) return Results.ValidationProblem(result.ToDictionary());
        var doctor = mapper.Map<Doctor>(doctorData);
        db.Doctors.Update(doctor);
        await db.SaveChangesAsync();
        return Results.Json(mapper.Map<UpdateDoctor>(doctor));
    })
    .Produces<UpdateDoctor>()
    .Produces(StatusCodes.Status400BadRequest)
    .WithTags("Doctors");

app.MapDelete("/api/doctors/{id:int}", async (int id, AppDb db) =>
    {
        var doctor = await db.Doctors.FindAsync(id);
        if (doctor == null) return Results.NotFound();
        db.Doctors.Remove(doctor);
        await db.SaveChangesAsync();
        return Results.NoContent();
    })
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound)
    .WithTags("Doctors");
#endregion

# region Patients api
app.MapGet("/api/patients", async (int page, int pageSize, string sortField, string sortOrder, IMapper mapper, AppDb db) => await db.Patients    
    .Include(d => d.Area)
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .OrderByDynamic(sortField, sortOrder)
    .Select(p => mapper.Map<IndexPatient>(p))
    .AsNoTracking()
    .ToListAsync())
    .WithTags("Patients");

app.MapGet("/api/patients/{id:int}", async (int id, IMapper mapper, AppDb db) =>
    {
        var patient = await db.Patients.FindAsync(id);
        if (patient == null) return Results.NotFound();
        return Results.Ok(mapper.Map<UpdatePatient>(patient));
    })
    .Produces<UpdatePatient>()
    .Produces(StatusCodes.Status404NotFound)
    .WithTags("Patients");

app.MapPost("/api/patients", async (CreatePatient patientData, IValidator<CreatePatient> validator, IMapper mapper, AppDb db) =>
    {
        var result = await validator.ValidateAsync(patientData);
        if (!result.IsValid) return Results.ValidationProblem(result.ToDictionary());
        var patient = mapper.Map<Patient>(patientData);
        await db.Patients.AddAsync(patient);
        await db.SaveChangesAsync();
        return Results.Created($"/api/patients/{patient.Id}", mapper.Map<UpdatePatient>(patient));
    })
    .Produces<UpdatePatient>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .WithTags("Patients");

app.MapPut("/api/patients", async (UpdatePatient patientData, IValidator<UpdatePatient> validator, IMapper mapper, AppDb db) =>
    {
        var result = await validator.ValidateAsync(patientData);
        if (!result.IsValid) return Results.ValidationProblem(result.ToDictionary());
        var patient = mapper.Map<Patient>(patientData);
        db.Patients.Update(patient);
        await db.SaveChangesAsync();
        return Results.Json(mapper.Map<UpdatePatient>(patient));
    })
    .Produces<UpdatePatient>()
    .Produces(StatusCodes.Status400BadRequest)
    .WithTags("Patients");

app.MapDelete("/api/patients/{id:int}", async (int id, AppDb db) =>
    {
        var patient = await db.Patients.FindAsync(id);
        if (patient == null) return Results.NotFound();
        db.Patients.Remove(patient);
        await db.SaveChangesAsync();
        return Results.NoContent();
    })
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound)
    .WithTags("Patients");
#endregion

app.Run();