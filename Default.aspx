<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Informasjon om valget</h2>
            <p>
               Her skal det stå en del av informasjonen man skal vite om valget. </br> Hvorfor vi har valget? </br> hvordan valget funker? </br>
    hva man kan gjøre med sine stemmer.
            </p>
        </div>
        <div class="col-md-4">
            <h2>info om noe</h2>
            <p>
                RESTE FESTE
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">bruk av link hvis trengs</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>info om noe annet</h2>
            <p>
                LA OSSS FESTE
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">bruk av link hvis trengs</a>
            </p>
        </div>
    </div>

</asp:Content>
