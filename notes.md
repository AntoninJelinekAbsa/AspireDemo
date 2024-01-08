Start postgres container:
```
docker run --hostname=21b227157a31 --mac-address=02:42:ac:11:00:04 --env=POSTGRES_PASSWORD=p@ssword123 --env=GOSU_VERSION=1.16 --env=LANG=en_US.utf8 --env=PG_MAJOR=16 --env=PG_VERSION=16.1-1.pgdg120+1 --env=PGDATA=/var/lib/postgresql/data --env=POSTGRES_USER=SeriesUser --env=POSTGRES_DB=SeriesDb --env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/usr/lib/postgresql/16/bin --volume=/var/lib/postgresql/data -p 5432:5432 --restart=no --runtime=runc -d postgres:latest
```


Aspire

Install workload (can be done via Visual Studio installer):
```
dotnet workload install aspire
```






Deployment
- manifest:
```
 dotnet run --publisher manifest --output-path aspire-manifest.json
```


How to prepare ollama model on Windows
https://ollama.ai/
Not available on Windows yet but there is an official docker container.
However, to work with it in Docker container, you'll likely not have internet access from within the container to pull the model from the internet. 
The problem is zscaler root certificate. What you can do is to create your own ollama container with proper certificate installed.
- find your zscaler root certificate as .cer file
- prepare your dockerfile:
```
FROM ollama/ollama
COPY ./zsc-cert.cer /etc/ssl/certs/zsc-cert.cer
COPY ./zsc-cert.cer /etc/ssl/certs/ca-certificates.crt
RUN update-ca-certificates
```

- build your image:
```
docker buildx build --rm  -t ollama-custom:latest .
```

- run your image:
```
docker run ollama-custom
```

- attach a shell to your docker container and run ollama commands:
```
ollama --help
ollama pull orca-mini     //pull orca-mini model. For list of available models, see https://github.com/jmorganca/ollama#model-library
```

Create a ``Modelfile`` inside your container:
```
FROM llama2

# set the temperature to 1 [higher is more creative, lower is more coherent]
PARAMETER temperature .7

# set the system prompt
SYSTEM 
You are George Raymond Richard Martin, recognized author of fantasy sagas. You are famous for writing very long novels. You are also extremely known for not finishing your novels. Answer as George Raymond Richard Martin, only.
```

Inside your conatiner, run:
```
ollama create grrmartin -f Modelfile	//create a model called grrmartin
```

Capture the state of your container:
```
docker ps   //to get the container id
docker commit <container-id> <your-model-name>:latest
```




Links:
- https://github.com/prom3theu5/aspirational-manifests -  Automate deployment of a .NET Aspire AppHost to a Kubernetes Cluster
