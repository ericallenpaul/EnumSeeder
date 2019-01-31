If NOT EXISTS (SELECT Id FROM [Department] WHERE Id = 1)
BEGIN
	INSERT INTO [dbo].[Department]
	(
		[Id]
		,[Name]
		,[Description]
	)
	VALUES
	(
		1
		,'Sales'
		,'Description'
	)
END