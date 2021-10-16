CREATE FUNCTION udf_GetCost(@JobId INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
	DECLARE @TotalCost Decimal(18,2);

	SET @TotalCost = ISNULL(
		(SELECT SUM(op.Quantity * p.Price) FROM Orders AS o
		INNER JOIN OrderParts AS op ON op.OrderId = o.OrderId
		INNER JOIN Parts AS p ON p.PartId = op.PartId
		WHERE JobId = @JobId), 0
	);

	RETURN @TotalCost
END
