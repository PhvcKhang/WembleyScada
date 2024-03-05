﻿global using MediatR;
global using AutoMapper;
global using OfficeOpenXml;
global using LicenseContext = OfficeOpenXml.LicenseContext;
global using Azure.Storage.Blobs;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using WembleyScada.Api.Application.Mapping;
global using WembleyScada.Api.Application.Queries.Stations;
global using WembleyScada.Api.Application.Queries.Employees;
global using WembleyScada.Api.Application.Queries.References;
global using WembleyScada.Api.Application.Queries.Lines;
global using WembleyScada.Api.Application.Queries.Products;
global using WembleyScada.Api.Application.Queries.StationReferences;
global using WembleyScada.Api.Application.Queries.ErrorInformations;
global using WembleyScada.Api.Application.Queries.MachineStatuses;
global using WembleyScada.Api.Application.Queries.ShiftReports;
global using WembleyScada.Api.Application.Queries.References.Parameters;
global using WembleyScada.Api.Application.Queries.ShiftReports.Details;
global using WembleyScada.Api.Application.Queries.ShiftReports.Download;
global using WembleyScada.Api.Application.Queries.ShiftReports.Lastest;
global using WembleyScada.Api.Application.Queries.ShiftReports.ShortenDetail;
global using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;
global using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
global using WembleyScada.Domain.AggregateModels.StationReferenceAggregate;
global using WembleyScada.Domain.AggregateModels.StationAggregate;
global using WembleyScada.Domain.AggregateModels.LineAggregate;
global using WembleyScada.Domain.AggregateModels.ProductAggregate;
global using WembleyScada.Domain.AggregateModels.EmployeeAggregate;
global using WembleyScada.Domain.AggregateModels.ReferenceAggregate;
global using WembleyScada.Infrastructure;



