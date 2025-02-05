﻿global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.Negotiate;
global using FileManager.Application.Interfaces;
global using FileManager.Application.DTO;
global using FileManager.Application.UseCases;
global using FileManager.Application.Models;
global using FileManager.Domain.Exceptions;
global using FileManager.Application.Services;
global using FileManager.Domain.Interfaces;
global using FileManager.Domain.Entities;
global using FileManager.Infrastructure.Persistence;
global using FileManager.Infrastructure.Persistence.Repositories;
global using FileManager.Infrastructure.Services;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.SqlServer;