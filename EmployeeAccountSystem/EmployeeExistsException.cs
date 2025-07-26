using System;

namespace EmployeeAccountSystem;

/// <summary>
/// Исключение при уже существующем сотруднике.
/// </summary>
public class EmployeeExistsException : Exception
{
    /// <summary>
    /// Создает новое исключение, указывающее, что сотрудник уже существует.
    /// </summary>
    /// <param name="message">Менеджер управления сотрудниками.</param>
    public EmployeeExistsException(string message) : base(message) { }
}