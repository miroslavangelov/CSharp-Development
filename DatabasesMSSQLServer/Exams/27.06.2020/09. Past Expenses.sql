SELECT j.[JobId], ISNULL(SUM(p.[Price] * op.[Quantity]), 0) AS [Total]
FROM [Jobs] as j
LEFT JOIN [Orders] as o ON o.[JobId] = j.[JobId]
LEFT JOIN [OrderParts] as op ON op.[OrderId] = o.[OrderId]
LEFT JOIN [Parts] as p ON p.[PartId] = op.[PartId]
WHERE j.[Status] = 'Finished'
GROUP BY j.[JobId]
ORDER BY [Total] DESC, j.[JobId];
