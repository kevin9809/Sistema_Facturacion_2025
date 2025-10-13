﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.WebForm1" %>
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
      <p>Envío rápidoO</p>
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

     <%-- Footer --%>
 <footer class="footer-wrapp" id="footer">
   <div class="row container footer-top ">
     <img
       src="https://res.cloudinary.com/dymsokiwr/image/upload/v1729572683/IF_Logo_BW_slqpml.png"
       alt=""
       class="col-sm-5 col-md"
     />
     <div class="col-sm-5 col-md">
       <div class="content">
         <p>Subscribe to our news letter</p>
         <div class="input-group">
           <input
             type="email"
             class="form-control"
             placeholder="Enter your email"
           ></input>
           <button class="btn subscribe-btn" type="submit">
             Subscribe Now
           </button>
         </div>
       </div>
     </div>
   </div>
   <div class="row footer-links justify-content-center">
     <div class="links col-2 text-center">
       <p class="title ">About Insight</p>
       <li>About</li>
       <li>Careers</li>
       <li>News</li>
       <li>About Insight</li>
     </div>
     <div class="links  col-2 text-center">
       <p class="title">Insight Help</p>
       <li>Accessibility</li>
       <li>Help Center</li>
       <li>FAQ</li>
       <li>Returns</li>
       <li>Price Match</li>
       <li>Child Safety</li>
     </div>
     <div class="links col-2 text-center">
       <p class="title">Services</p>
       <li>Financing</li>
       <li>Rewards</li>
       <li>Trade Program</li>
       <li>Delivery Methods</li>
     </div>
     <div class="links  col-2 text-center">
       <p class="title">Legal</p>
       <li>Terms and Conditions</li>
       <li>Offers & Details</li>
       <li>Privacy Policy</li>
     </div>
   </div>
 </footer> 

</asp:Content>
