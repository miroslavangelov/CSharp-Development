CREATE PROC usp_PlaceOrder(@JobId INT, @SerialNumber VARCHAR(50), @Quantity INT) AS
BEGIN
	DECLARE @JobStatus VARCHAR(11) = (SELECT Status FROM Jobs WHERE JobId = @JobId)
	DECLARE @JobExists INT = (SELECT COUNT(JobId) FROM Jobs WHERE JobId = @JobId)
	DECLARE @PartExists INT = (SELECT COUNT(PartId) FROM Parts WHERE SerialNumber = @SerialNumber)
	
	IF (@JobStatus = 'Finished')
	BEGIN;
		THROW 50011, 'This job is not active!', 1
	END

	IF (@Quantity <= 0)
	BEGIN;
		THROW 50012, 'Part quantity must be more than zero!', 1
	END

	IF (@JobExists = 0)
	BEGIN;
		THROW 50013, 'Job not found!', 1
	END

	IF (@PartExists = 0)
	BEGIN;
		THROW 50014, 'Part not found!', 1
	END

	DECLARE @OrderExists INT = (SELECT COUNT(OrderId) FROM Orders WHERE JobId = @JobId AND IssueDate IS NULL)

	IF (@OrderExists = 0)
	BEGIN;
		INSERT INTO Orders VALUES
		(@JobId, NULL, 0)
	END

	DECLARE @OrderId INT = (SELECT OrderId FROM Orders WHERE JobId = @JobId AND IssueDate IS NULL)

	IF (@OrderId > 0)
	BEGIN;
		DECLARE @PartId INT = (SELECT PartId FROM Parts WHERE SerialNumber = @SerialNumber)
		DECLARE @PartExistsInOrder INT = (SELECT COUNT(PartId) FROM OrderParts WHERE OrderId = @OrderId AND PartId = @PartId)

		IF (@PartExistsInOrder > 0)
		BEGIN
			UPDATE OrderParts
			SET Quantity += @Quantity
			WHERE OrderId = @OrderId AND PartId = @PartId
		END
		ELSE
		BEGIN
			INSERT INTO OrderParts VALUES
			(@OrderId, @PartId, @Quantity)
		END
	END
END
