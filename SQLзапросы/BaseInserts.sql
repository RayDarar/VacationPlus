use VPDB
INSERT INTO AdminLogin(login, password) VALUES('admin', 'admin')
INSERT INTO Countries(name, description) VALUES('Казахстан', 'Многонациональная, развивающаяся страна')
INSERT INTO Countries(name, description) VALUES('Россия', 'Самая большая страна в мире')
INSERT INTO Cities(name, description, countryID) VALUES('Алматы', 'Город яблок и велосипедов', 1)
INSERT INTO Cities(name, description, countryID) VALUES('Астана', 'Город столица в буквальном и переносном смысле', 1)
INSERT INTO Cities(name, description, countryID) VALUES('Москва', 'Столица, население которой больше казахстана', 2)
INSERT INTO Departments(name, description) VALUES('Отдел по продажам', 'Центральный отдел по продажам')
INSERT INTO Departments(name, description) VALUES('Отдел по налогам', 'Центральный отдел по налогм')