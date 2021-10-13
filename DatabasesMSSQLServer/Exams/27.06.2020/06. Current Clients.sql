SELECT CONCAT(c.[FirstName], ' ', c.[LastName]) AS [Client],
DATEDIFF(DAY, j.[IssueDate], '04/24/2017') As [Days going], j.[Status]
FROM [Clients] as c
INNER JOIN [Jobs] as j ON c.[ClientId] = j.[ClientId]
WHERE j.[Status] != 'Finished'
ORDER BY [Days going] DESC, c.[ClientId];
