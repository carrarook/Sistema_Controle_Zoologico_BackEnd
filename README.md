🦁 Sistema de Controle Zoológico - Backend
Backend da aplicação de gerenciamento de animais e cuidados, desenvolvido em .NET 9 com banco de dados SQLite.

👉 Frontend: [sistemazoologicofront.vercel.app](https://sistemazoologicofront.vercel.app/)

📦 Repositório do Frontend: [zoo-management-client](https://github.com/carrarook/zoo-management-client)

📦 Repositório do Back: [Sistema_Controle_Zoologico_BackEnd](https://github.com/carrarook/Sistema_Controle_Zoologico_BackEnd)

-------- Funcionalidades -------- 

* Cadastrar animais

* Cadastrar cuidados

* Relacionar cuidados aos animais

* Dashboard com informações resumidas

-------- Tecnologias -------- 

* .NET 9

* Entity Framework Core

* SQLite

* API REST

-------- Problemas conhecidos -------- 

❗ Login/Signup foi removido temporariamente devido a erro de CORS no Azure (funciona localmente).

⚠️ SQLite no Azure Free Tier não persiste dados em alguns casos.

-------- Melhorias futuras -------- 

* Corrigir CORS para login no Azure

* Trocar SQLite por banco com persistência garantida

* Implementar autenticação completa

-------- Como rodar localmente --------

1. Clone o backend e o frontend
2. No frontEnd, altere o endereço da API nos arquivos Services (apenas descomentar o teste e comentar a produção), confira o endereço local e altere para sua porta se diferente
3. Certifique-se de quem todas as bibliotecas necessárias, assim como o .net
4. Rode o .net (Atente-se a porta que está rodando)
5. rode o front ent (npm start)
6. Se quiser conferir as funcionalidades que funcionam localmente, troque o routes.jsx pelo arquivo .txt na mesma pasta
7. Thanks

