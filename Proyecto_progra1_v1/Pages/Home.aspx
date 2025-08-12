<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <!-- Sección 1 -->
 
<section class="section-full bg-light">
  <div class="container-fluid p-0 d-flex flex-wrap align-items-center">
    <!-- Imagen -->
    <div class="section-image col-md-9 p-0">
      <img src="/Imagenes/pic2.jpg" type="image" class="img-fluid w-100 h-100 object-fit-expand" alt="">
    </div>
    <!-- Información -->
    <div class="section-info col-md-3 d-flex flex-column justify-content-center p-4 text-center">
      <h2>Muebles Modernos</h2>
      <p>Descubre nuestra colección de muebles de última tendencia para tu hogar.</p>
      <a href="#" class="btn my-btn">Comprar Ahora</a>
    </div>
  </div>
</section>

<!-- Sección 2 (Transición con beneficios) -->
<section class="my-bg section-half text-center py-5">
  <h2 class="mb-4">¿Por qué elegirnos?</h2>
  <p class="mb-5">Calidad, financiamiento y entrega rápida para tu comodidad.</p>
  <div class="d-flex justify-content-center gap-5 row">
    <div class="col">
      <i class="bi bi-truck display-4"></i>
      <p>Envío rápido</p>
    </div>
    <div class="col">
      <i class="bi bi-cash-stack display-4"></i>
      <p>Financiamiento</p>
    </div>
    <div class="col">
      <i class="bi bi-award display-4"></i>
      <p>Garantía de calidad</p>
    </div>
  </div>
</section>

<!-- Sección 3 -->
<section class="section-full bg-light">
  <div class="container-fluid p-0 d-flex flex-wrap align-items-center flex-row-reverse">
    <!-- Imagen -->
    <div class="section-image col-md-9 p-0">
      <img src="/Imagenes/pic3.jpg" class="img-fluid w-100 h-100 object-fit-cover" alt="Mueble elegante">
    </div>
    <!-- Información -->
    <div class="section-info col-md-3 d-flex flex-column justify-content-center p-4 text-center">
      <h2>Muebles Elegantes</h2>
      <p>Diseños sofisticados para transformar tu hogar.</p>
      <a href="#" class="btn my-btn">Explorar</a>
    </div>
  </div>
</section>

<!-- Sección 4 (Transición con beneficios) -->
<section class="my-bg section-half text-center py-5">
  <h2 class="mb-4">Financiamiento Fácil</h2>
  <p class="mb-5">Paga en cómodas cuotas con nuestro plan especial para clientes.</p>
  <div class="d-flex justify-content-center gap-5 row">
    <div class="col">
      <i class="bi bi-credit-card display-4"></i>
      <p>Pagos flexibles</p>
    </div>
    <div class="col">
      <i class="bi bi-people display-4"></i>
      <p>Atención personalizada</p>
    </div>
    <div class="col">
      <i class="bi bi-house-heart display-4"></i>
      <p>Diseños exclusivos</p>
    </div>
  </div>
</section>

<!-- Sección 5 -->
<section class="section-full bg-light">
  <div class="container-fluid p-0 d-flex flex-wrap align-items-center">
    <!-- Imagen -->
    <div class="section-image col-md-9 p-0">
      <img src="/Imagenes/pic4.jpg" class="img-fluid w-100 h-100 object-fit-cover" alt="Mueble clásico">
    </div>
    <!-- Información -->
    <div class="section-info col-md-3 d-flex flex-column justify-content-center p-4 text-center">
      <h2>Muebles Clásicos</h2>
      <p>Estilo y comodidad para todos los gustos.</p>
      <a href="#" class="btn my-btn">Ver más</a>
    </div>
  </div>
</section>


</asp:Content>
