# task-management-service

> API para gerenciamento de tarefas.
>
> O objetivo é permitir aos usuários organizar e monitorar suas tarefas diárias, bem como colaborar com colegas de equipe.

## Detalhes do App

**Usuário:** *Pessoa que utiliza o aplicativo detentor de uma conta.*

**Projeto:** *Um projeto é uma entidade que contém várias tarefas. Um usuário pode criar, visualizar e gerenciar vários projetos.*

**Tarefa:** *Uma tarefa é uma unidade de trabalho dentro de um projeto.*

## API Reference

- ASP.NET Core Web API (.Net 6)
- Swashbuckle.Aspnetcore 6.5.0

## Database 📂

- Esta API usa o MySql(8.0) como database.  
- EntityFrameworkCore 7.0.14
- Pomelo.EntityFrameworkCore.MySql 7.0.0

## Tests Reference
- ASP .NET Core (.NET 6)
- Bogus 34.0.2
- Fluentassertions 6.12.0
- Moq 4.20.70
- xUnit 2.4.2

## Local Usage

**Pre requisitos**
- SDK do .NET 6
- Um editor de código .NET ou IDE (por exemplo, VS Code com o plugin C#, Visual Studio ou JetBrains Rider)
- Docker Engine. Você pode instalar o mecanismo usando Docker Desktop (Windows, macOS e Linux), Colima (macOS e Linux) ou manualmente em qualquer sistema operacional.
- Alguma experiência com Web API e EF é recomendada, mas não obrigatória.

**1. Subir container**

Navegue até a pasta raiz do projeto e execute `docker-compose up -d`

![image](https://github.com/anderson-araujo-cavalcante/task-management-service/assets/133878123/0c92ab9c-94d8-43c4-af47-821f2f4a4a20)

As imagens serão criadas e iniciadas

![image](https://github.com/anderson-araujo-cavalcante/task-management-service/assets/133878123/4adabc61-fcce-4f05-8e78-c568195a14a1)

**2. Database**

Ao subir a aplicação será executado as migrations de forma automática, neste momento será criado as entidades na base de dados;
   
**3. Navegação**

Abri o navegador e acessar a url ```http://localhost:8000/swagger/index.html```

![image](https://github.com/anderson-araujo-cavalcante/task-management-service/assets/133878123/8b3edb2f-16d3-4906-b9d9-c82a6f3aef04)



# Fase 2: Refinamento

> Refinamento de negócio para futuras implementações ou melhorias.

1. Porque não é permitido alterar a prioridade da tarefa? o negocio muda conforme a necessidade e as tarefas seguem suas prioridades.
1. No relatório terá a opção de visualizar tarefas que estão em atraso? por exemplo tarefas que estão "vencidas" há muito tempo ou tarefas que estão próximo de vencer;
1. É valido termos um status geral do projeto? onde será apresentado a situação atual do projeto de acordo com todas tarefas associadas a ele;
1. Ao remover um projeto é interessante incluir um comentário contendo motivo da exclusão?
1. Poderá incluir comentários após a tarefa estar concluida?
1. O limite de tarefas por projeto (máximo de 20) deve considerar todas tarefas independente de seus status? ou tarefas concluidas devem ser desconsideradas?



# Fase 3: Melhorias técnicas

> Possíveis pontos de melhoria, implementação de padrões, visão do projeto sobre arquitetura/cloud, etc

1. Centralizar as mensagem de erros em um resource;
1. Criar `exceptions` customizadas;
1. Melhorar resposta de erro da API, sugestão usar `options filters`;
1. Usar `FluentValidation` para tratar algumas regras na requisição;
1. Melhorar cobertura de testes (controllers, extensions, repositories, )
1. Adicionar testes de integração para garantir qualidade so software;
1. Possibilidade de criar um Client? poderiamos pensar em nuget packege;
1. Automatizar implantação com git actions workflows (pull, push, deploy, testes..);
1. Criar template para abertura de PULL REQUESTs;
1. Teremos COREOWNERS para o repósritorio?
1. Serão necessários quantos aprovadores (CODE REVIEW) para liberar merge?
1. Usar ferramenta para analisa de código, cobertura de testes, segurança, etc, por exemplo [SonarQube](https://www.sonarsource.com/products/sonarqube/?gads_campaign=SQ-Mroi-PMax&gads_ad_group=Global&gads_keyword=&cq_src=google_ads&cq_cmp=20184933017&cq_con=&cq_term=&cq_med=&cq_plac=&cq_net=x&cq_pos=&cq_plt=gp&gad_source=1&gclid=Cj0KCQiA67CrBhC1ARIsACKAa8S-mtsRm7C_qdNgERvCN_DamH_TzCNKdEz15_0VWKm-HawYrOZEmMYaAqY1EALw_wcB);
1. Usar ferramenta para auxiliar no deploy, por exemplo [Octopus](https://octopus.com/)
1. Usar ferramenta para integração continua, por exemplo o [TeamCity](https://www.jetbrains.com/pt-br/teamcity/);
1. Add logs para auxiliar rastreabilidade de bugs e visualiza-los através de serviços como [kibana](https://www.elastic.co/pt/kibana);
1. Usar ferramenta para testes de performance, por exemplo [K6](https://k6.io/)

