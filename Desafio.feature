Funcionalidade: Desafio

Cenário: Preencher e submeter formulário completo
Dado que acessei a página do formulário
E preenchi todos os campos
Quando clicar em salvar
Então Mensagem de sucesso deve ser exibida.

Cenário: Submeter formulário sem preencher os campos obrigatórios
Dado que acessei a página do formulário
Quando clicar em salvar
Então Mensagens de erro de campos obrigatórios não preenchidos devem ser exibidas.

Cenário: Submeter formulário com email inválido
Dado que acessei a página do formulário
E preenchi todos os campos
Quando clicar em salvar
Então Mensagem de erro de email inválido deve ser exibida.

Cenário: Submeter formulário com data inválida
Dado que acessei a página do formulário
E preenchi todos os campos
Quando clicar em salvar
Então Mensagem de erro de data inválida deve ser exibida.

Cenário: Submeter formulário com hora inválida
Dado que acessei a página do formulário
E preenchi todos os campos
Quando clicar em salvar
Então Mensagem de erro de hora inválida deve ser exibida.

Cenário: Selecionar opção "Other" sem preencher a sobremesa favorita
Dado que acessei a página do formulário
E preenchi todos os campos
Mas não preenchi o nome da Sobremesa
Quando clicar em salvar
Então Mensagens de erro de campos obrigatórios não preenchidos devem ser exibidas.

Cenário: Selecionar a mesma nota para dois Esportes diferentes
Dado que acessei a página do formulário
E preenchi todos os campos
Quando clicar em salvar
Então Mensagens de erro de mesma nota escolhida para esportes diferentes deve ser exibida.