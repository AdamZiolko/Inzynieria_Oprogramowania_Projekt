# Migracje bazodanowe
    dotnet tool install --global dotnet-ef


# Tworzenie migracji
	dotnet ef migrations add InitialCreate

# Aktualizacja bazy danych (może być z nazwą migracji, aby cofnąc zmiany w bazie danych)
	dotnet ef database update

# Cofanie migracji
	dotnet ef migrations remove

