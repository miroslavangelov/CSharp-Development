SELECT CONCAT(m.[FirstName], ' ', m.[LastName]) AS [Mechanic],
AVG(DATEDIFF(DAY, j.[IssueDate], j.[FinishDate])) As [Average Days]
FROM [Mechanics] as m
INNER JOIN [Jobs] as j ON m.[MechanicId] = j.[MechanicId]
WHERE j.[Status] = 'Finished'
GROUP BY m.[FirstName], m.[LastName], m.[MechanicId]
ORDER BY m.[MechanicId];
