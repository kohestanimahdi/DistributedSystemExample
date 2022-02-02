#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM python:latest



# Any working directory can be chosen as per choice like '/' or '/home' etc
# i have chosen /usr/app/src
WORKDIR /src

#to COPY the remote file at working directory in container
COPY DistributedSystems.NotifierService/DistributedSystems.NotifierService.py ./
COPY DistributedSystems.NotifierService/appsettings.json ./
COPY DistributedSystems.NotifierService/requirements.txt ./
# Now the structure looks like this '/usr/app/src/test.py'

RUN pip install -r requirements.txt
#CMD instruction should be used to run the software
#contained by your image, along with any arguments.

CMD [ "python", "./DistributedSystems.NotifierService.py"]