SELECT CONCAT(m.[FirstName], ' ', m.[LastName]) AS Mechanic,
j.[Status], j.[IssueDate]
FROM [Mechanics] as m
INNER JOIN [Jobs] as j ON m.[MechanicId] = j.[MechanicId]
ORDER BY m.[MechanicId], j.[IssueDate], j.[JobId];
