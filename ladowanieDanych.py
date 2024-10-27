import json
import pyodbc


with open('data2.json', 'r', encoding='utf-8') as file:
    book_datas = json.load(file)


connection_string = (
    "Driver={ODBC Driver 17 for SQL Server};"
    "Server=(LocalDb)\\MSSQLLocalDB;"
    "Database=project_bookworm;"
    "Trusted_Connection=yes;"
    "TrustServerCertificate=yes;"
)

conn = pyodbc.connect(connection_string)
cursor = conn.cursor()



for book_data in book_datas:
    try:
        release_date = f"{book_data['ReleaseDate']}-01-01"

        cursor.execute("""
            INSERT INTO Books (Title, Author, Genre, ReleaseDate)
            OUTPUT INSERTED.Id
            VALUES (?, ?, ?, ?)
            """,
            book_data["title"],
            book_data["Author"],
            book_data["Genre"],
            release_date
        )

        book_id = cursor.fetchone()[0]

        cursor.execute("""
            INSERT INTO BookContents (Content, BookId)
            VALUES (?, ?)
            """,
            book_data["content"],
            book_id
        )

        conn.commit()
    except Exception as e:
        print(f"An error occurred: {e}")


conn.close()