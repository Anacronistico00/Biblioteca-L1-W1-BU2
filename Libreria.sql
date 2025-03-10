USE Biblioteca;

SELECT * FROM Books

INSERT INTO Books (Id, Title, Author, Genre, Available, CoverUrl) 
VALUES
    (NEWID(), 'Il nome della rosa', 'Umberto Eco', 'Mistero', 1, 'https://m.media-amazon.com/images/I/411YxyolifL._SY445_SX342_.jpg'),
    (NEWID(), '1984', 'George Orwell', 'Distopia', 1, 'https://m.media-amazon.com/images/I/41OQfYo+GrL._SY445_SX342_.jpg'),
    (NEWID(), 'Orgoglio e Pregiudizio', 'Jane Austen', 'Romanzo', 0, 'https://m.media-amazon.com/images/I/51czK2DzP7L._SY445_SX342_.jpg'),
    (NEWID(), 'Il Signore degli Anelli', 'J.R.R. Tolkien', 'Fantasy', 1, 'https://m.media-amazon.com/images/I/81RabOfQcNL._SY466_.jpg'),
    (NEWID(), 'Harry Potter e la Pietra Filosofale', 'J.K. Rowling', 'Fantasy', 1, 'https://m.media-amazon.com/images/I/51CgZFxNQ0L._SY445_SX342_.jpg'),
    (NEWID(), 'Moby Dick', 'Herman Melville', 'Avventura', 1, 'https://m.media-amazon.com/images/I/41p7juf3jgL._SY445_SX342_.jpg'),
    (NEWID(), 'Il piccolo principe', 'Antoine de Saint-Exupéry', 'Narrativa', 1, 'https://m.media-amazon.com/images/I/41+rtzsPCkL._SY445_SX342_.jpg'),
    (NEWID(), 'Dracula', 'Bram Stoker', 'Horror', 0, 'https://m.media-amazon.com/images/I/41lvBN8z3PL._SY445_SX342_.jpg');
