# Sistema de Gestão de Refeições e Receitas

Este projeto é um **aplicativo de planejamento de refeições (Meal Planner)** desenvolvido em **C#**, que permite ao usuário **gerenciar receitas, planejar menus e calcular informações nutricionais e ambientais**.

---

## 📌 Funcionalidades Principais

O sistema funciona por meio de um **menu em console**, oferecendo as seguintes ações:

### 1. Cadastro de Receita

O usuário pode:

* Inserir o **nome da receita**
* Adicionar **tags** (ex.: vegetariano, light)
* Inserir **ingredientes**, informando:

  * Calorias
  * *Environmental Impact Score* (impacto ambiental)

### 2. Listagem de Receitas

Exibe todas as receitas cadastradas no sistema.

### 3. Sugestão de Receitas

O **MealPlanner** sugere receitas com base nas **preferências informadas pelo usuário**, armazenadas na classe `User`.

### 4. Criação de Menu e Lista de Compras

O usuário pode criar um **Menu** contendo várias receitas.
O sistema gera uma **GroceryList** consolidando todos os ingredientes necessários.

### 5. Cálculo Nutricional

A classe `NutritionCalculator` soma as **calorias totais** da receita.

### 6. Cálculo de Sustentabilidade

A classe `SustainabilityCalculator` calcula a **média do impacto ambiental** dos ingredientes.

---

## 🧱 Estrutura de Classes

O programa é dividido nas seguintes classes:

* **Program**: Contém o `Main` e o menu do usuário.
* **DataStore**: Simula um banco de dados de receitas.
* **Recipe**: Armazena nome, tags e lista de ingredientes.
* **Ingredient**: Armazena nome, calorias e `EnvironmentalImpactScore`.
* **User**: Representa o usuário e suas preferências.
* **MealPlanner**: Lógica de sugestão de receitas.
* **Menu**: Agrupa um conjunto de receitas.
* **GroceryList**: Gera e imprime a lista de ingredientes consolidada.
* **NutritionCalculator** e **SustainabilityCalculator**: Executam cálculos específicos.

📌 **Total de métodos no projeto:** 26.

**Casos de Teste – Sistema de Gestão de Refeições e Receitas**
*Validação das principais funcionalidades do sistema.*

---

# **Caso de Teste 1**

### **CT01 – Cadastro de Receita**

**Objetivo:** Verificar se o sistema cadastra uma nova receita corretamente.
**Entradas:** Nome da receita, ingredientes, tempo de preparo, categoria.
**Passos:**

1. Acessar o menu “Cadastrar Receita”.
2. Inserir os dados obrigatórios.
3. Confirmar o cadastro.
   **Resultado Esperado:** Receita salva e exibida na lista.

---

# **Caso de Teste 2**

### **CT02 – Edição de Receita**

**Objetivo:** Validar se uma receita existente pode ser editada.
**Entradas:** Nova descrição, novos ingredientes.
**Passos:**

1. Selecionar receita existente.
2. Alterar algum campo.
3. Salvar alterações.
   **Resultado Esperado:** Receita atualizada corretamente.

---

# **Caso de Teste 3**

### **CT03 – Exclusão de Receita**

**Objetivo:** Garantir que o sistema exclui uma receita.
**Passos:**

1. Selecionar receita específica.
2. Confirmar remoção.
   **Resultado Esperado:** Receita não aparece mais na lista.

---

# **Caso de Teste 4**

### **CT04 – Listagem de Receitas**

**Objetivo:** Testar se o sistema exibe todas as receitas cadastradas.
**Passos:**

1. Acessar menu “Listar Receitas”.
   **Resultado Esperado:** Lista completa e atualizada.

---

# **Caso de Teste 5**

### **CT05 – Planejar Refeição**

**Objetivo:** Validar criação de um plano de refeição usando receitas existentes.
**Passos:**

1. Acessar “Planejar Refeição”.
2. Selecionar receitas.
3. Confirmar o plano.
   **Resultado Esperado:** Plano criado e exibido.

---

# **Caso de Teste 6**

### **CT06 – Cálculo Nutricional**

**Objetivo:** Testar o cálculo automático dos valores nutricionais.
**Entradas:** Ingredientes e quantidades.
**Resultado Esperado:** Exibição correta de calorias, proteínas etc.

---

# **Caso de Teste 7**

### **CT07 – Cálculo de Sustentabilidade**

**Objetivo:** Verificar cálculo de pegada de carbono e impacto sustentável.
**Entradas:** Ingredientes e métricas ambientais.
**Resultado Esperado:** Exibição do valor ambiental correspondente.

---

# **Caso de Teste 8**

### **CT08 – Navegação pelo Menu**

**Objetivo:** Garantir que o menu de console funciona corretamente.
**Passos:**

1. Navegar pelas opções do menu.
   **Resultado Esperado:** Cada opção redireciona para a funcionalidade correta.

---

# **Caso de Teste 9**

### **CT09 – Validação de Campos Obrigatórios**

**Objetivo:** Certificar que o sistema bloqueia cadastros incompletos.
**Passos:**

1. Tentar cadastrar receita sem nome.
   **Resultado Esperado:** Exibição de mensagem de erro.

---

## 🛠️ Tecnologias Utilizadas

* **C#**
* **.NET 8**
* **Visual Studio**
* **ReportGenerator**
  [https://github.com/danielpalme/ReportGenerator](https://github.com/danielpalme/ReportGenerator)
* **XUnit**
* **GitHub do projeto:**
  [https://github.com/Gachaves/Sistema-de-Gest-o-Refei-es-e-Receitas..git](https://github.com/Gachaves/Sistema-de-Gest-o-Refei-es-e-Receitas..git)

---

## 🧪 Cálculo de Teste
<img width="1600" height="835" alt="image" src="https://github.com/user-attachments/assets/b90d60c4-7fec-406c-9aaf-fb757de06d64" />

<img width="1600" height="566" alt="image" src="https://github.com/user-attachments/assets/69b11305-7e82-4639-ba57-655ad9647eb5" />

<img width="855" height="661" alt="image" src="https://github.com/user-attachments/assets/fc54dc34-4aef-4194-84d7-57a4d3055e07" />


https://youtu.be/BI53C_jz3SM
