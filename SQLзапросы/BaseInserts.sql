use VPDB
INSERT INTO AdminLogin(login, password) VALUES('admin', 'admin')
INSERT INTO Countries(name, description) VALUES('���������', '�����������������, ������������� ������')
INSERT INTO Countries(name, description) VALUES('������', '����� ������� ������ � ����')
INSERT INTO Cities(name, description, countryID) VALUES('������', '����� ����� � �����������', 1)
INSERT INTO Cities(name, description, countryID) VALUES('������', '����� ������� � ���������� � ���������� ������', 1)
INSERT INTO Cities(name, description, countryID) VALUES('������', '�������, ��������� ������� ������ ����������', 2)
INSERT INTO Departments(name, description) VALUES('����� �� ��������', '����������� ����� �� ��������')
INSERT INTO Departments(name, description) VALUES('����� �� �������', '����������� ����� �� ������')