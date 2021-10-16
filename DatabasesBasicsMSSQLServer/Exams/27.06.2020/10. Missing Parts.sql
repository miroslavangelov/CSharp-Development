SELECT pn.[PartId], p.[Description], pn.[Quantity] AS [Required],
p.[StockQty] AS [In Stock], ISNULL(op.[Quantity], 0) AS [Ordered]
FROM [PartsNeeded] as pn
LEFT JOIN [Orders] as o ON o.[JobId] = pn.[JobId]
LEFT JOIN [OrderParts] as op ON op.[OrderId] = o.[OrderId]
LEFT JOIN [Parts] as p ON p.[PartId] = pn.[PartId]
LEFT JOIN [Jobs] as j ON j.[JobId] = pn.[JobId]
WHERE j.[Status] != 'Finished'
AND p.[StockQty] + ISNULL(op.[Quantity], 0) < pn.[Quantity]
ORDER BY pn.[PartId];
