using System;

namespace EmployeeAccountSystem;

/// <summary>
/// Исключение при не найденном сотруднике.
/// </summary>
public class EmployeeNotFoundException : Exception
{
    /// <summary>
    /// Создает новое исключение, указывающее, что сотрудник не найден.
    /// </summary>
    /// <param name="message">Менеджер управления сотрудниками.</param>
    public EmployeeNotFoundException(string message) : base(message) { }
}