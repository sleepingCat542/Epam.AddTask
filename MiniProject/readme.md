����-������ "���������" 

File Cabinet
��������

����������
��� 1

������ � ��������, ������������ ������, List
������� create

> create
First name: ...
Last name: ...
Date of birth: ...
Record #1 is created.

������� list

> list
#1, John, Doe
#2, Stan, Smith

������� stat

> stat
3 records.

��� 2
������� find

> find firstname "John"
#1
#2

������� edit

> edit #1
First name: ...
Last name: ...
Date of birth: ...

��� 3 -
������� export csv

> export csv

������� export xml

> export csv

��� 4 -

���������� �������� �������
��� 5
���������� ������� find

> find firstname "John", lastname "Doe"
#1
#2

���������� ������� list

> list id, firstname, lastname
#1, John, Doe
#2, Stan, Smith

��� 6 -

���������� in memory ������ ��������.
��� 7
������� import csv

import csv filename.csv

������� import xml

import xml filename.xml

��� 8
������� remove

> remove #1
Record #1 is removed.

������� purge