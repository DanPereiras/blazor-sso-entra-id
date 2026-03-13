# Case de Portfólio: Implementação de SSO com Microsoft Entra ID em Blazor Server

## 📌 Visão Geral
Este projeto apresenta a implementação de um sistema de **Single Sign-On (SSO)** utilizando **Microsoft Entra ID (Azure AD)** em uma aplicação **Blazor Server (.NET 8/9)**. O foco principal é a segurança de identidade, garantindo que apenas usuários autenticados da organização acessem a plataforma, utilizando protocolos modernos de autenticação e autorização.

---

## 🛡️ O Desafio (The Problem)
A gestão fragmentada de usuários em aplicações corporativas gera riscos de segurança, como senhas fracas, dificuldade no desligamento de colaboradores e falta de auditoria centralizada.
- **Risco:** Autenticação local vulnerável a ataques de força bruta.
- **Complexidade:** Necessidade de gerenciar múltiplos bancos de dados de usuários.
- **Conformidade:** Dificuldade em atender requisitos de auditoria que exigem MFA (Autenticação de Múltiplos Fatores) e logs centralizados.

---

## 🚀 A Solução (The Solution)
Implementei a integração nativa com o **Microsoft Entra ID**, transformando a aplicação em um cliente confiável do ecossistema de identidade da Microsoft.

### Principais Funcionalidades:
1.  **Autenticação Centralizada (SSO):** Login único integrado às credenciais corporativas do Windows/Office 365.
2.  **Integração com Microsoft Graph API:** Recuperação segura de dados do perfil do usuário (nome, e-mail, foto) diretamente do diretório Azure.
3.  **Gestão de Tokens Segura:** Implementação de cache de tokens em memória e aquisição de tokens para chamadas de APIs downstream.
4.  **Políticas de Autorização:** Configuração de `FallbackPolicy` para garantir que toda a aplicação seja protegida por padrão (Secure by Default).
5.  **MFA Nativo:** Suporte automático a Autenticação de Múltiplos Fatores configurada no Tenant da organização.

---

## 🛠️ Tecnologias e Protocolos
- **Framework:** .NET 8/9 (Blazor Server)
- **Provedor de Identidade:** Microsoft Entra ID (Azure AD)
- **Protocolos:** OpenID Connect (OIDC) e OAuth 2.0
- **Bibliotecas:** 
  - `Microsoft.Identity.Web` (Integração Azure AD)
  - `Microsoft.Graph` (Acesso a dados do diretório)
  - `Microsoft.Identity.Web.UI` (Componentes de interface de login)

---

## ⚙️ Destaques da Implementação
No arquivo `Program.cs`, a segurança foi configurada seguindo as melhores práticas de **AppSec**:

```csharp
// Configuração do Middleware de Autenticação e Graph API
builder.Services
    .AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi(scopes)
    .AddDownstreamApi("GraphApi", builder.Configuration.GetSection("GraphApi"))
    .AddInMemoryTokenCaches();

// Política de Segurança: Bloqueio total por padrão
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});
```

---

## 📈 Impacto e Valor de Negócio
- **Segurança:** Implementação de MFA e políticas de acesso condicional sem necessidade de código adicional.
- **Governança:** Controle total sobre quem acessa a aplicação via portal Azure.
- **Experiência do Usuário:** Login transparente e sem fricção para funcionários já logados em suas contas Microsoft.

---

## 💡 Lições de Cibersegurança
Este projeto reforça o conceito de **Identity as the New Perimeter** (A Identidade como o Novo Perímetro). Ao delegar a autenticação para um provedor robusto como o Entra ID, reduzimos a superfície de ataque da aplicação e garantimos conformidade com normas como **ISO 27001** e **LGPD**.

---

> **Nota de Carreira:** A habilidade de integrar aplicações com provedores de identidade em nuvem é essencial para profissionais de **IAM (Identity and Access Management)** e **Segurança de Aplicações**.
