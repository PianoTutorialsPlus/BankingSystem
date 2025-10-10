# BankingSystem

This project contains a test banking system with the following architecture:

- WPF
- REST API
- Clean Architecture

These patterns are included:

- CQRS 
- MVVM
- User Controls for separating UI elements
- DTOs

## Time Effort

- 10 min (initial prompt)
- 10 min (evaluating the result)
- 10 min (extending the prompt + evaluating the added/updated parts)
- 20 min (setup the project, adding references, adding folders)
- 40 min (filling the classes from the prompt)
- ca 30-60 min (debugging, adapting, etc)

## The Prompts
These prompts were used to complete the task:

### Initial prompt

" You are a c# software developer, specialized in wpf desktop application development. You get the following task: Developing banking software The software should include teh following: - WPF - Storing data in DataTable/DataSet - Connection to an SQL database (SQLite) The following use cases should be implemented: 
- *Original setup in all aspects*
    

Use the following: 
- WPF with MVVM, including Clean Architecture 
- Use the following Layers: 
    - BankingSystem.Api 
    - BankingSystem.Application 
    - BankingSystem.Domain 
    - BankingSystem.Persistence 
    - BankingSystem.UI 
- Use SQLite as database 
- Use Autofac as dependency Injection 
- Use CQRS 
- Use REST 
- Use DTO´s where it is suitable"

### Prompt after evaluating

"I need to update the task first: 
- For UI, use a cominations of Windows and UserControls for SRP, starting with a ShellMainView 
- Implement a NavigationService for linking the Views to their corresponding ViewModels 

Second: Full repo scaffold (all files, ready to copy)

