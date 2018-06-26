# Api Mundipag Message Translator
Web Api com autorização Basic e tradutor de mensagens com assinatura única para saída

## Etapas para geraçao de uma nova mensagem.
1.	Criação de um novo contrato de mensagens no projeto MundiPag.MessageTranslator.SharedKernel, na pasta Contracts;
2.	Criação das composições do contrato(Entidades) na pasta Compositions;
3.	Criação do agregador de composições(Implementação do contrato criado no item 1);
4.	Criação de adaptador de dados, onde será retornado a mensagem enriquecida/alterada, na pasta Adapters;
5.	Criação do tradutor de mensagens(formatação em json) para mensagens de entrada no formato XML.

## Autenticação Basic
1. Foi criado um filtro na pasta Authorization, no projeto MundiPag.MessageTranslator.Api, onde o filtro tem como finalidade identificar o usuário e senha fornecidos no token da requisição a um determinado endpoint;
2. Foi anotado na classe controladora o atributo BasicAuthorize, que tem como objetivo realizar a chamada para o filtro de autorização.
Validando o usuário.
