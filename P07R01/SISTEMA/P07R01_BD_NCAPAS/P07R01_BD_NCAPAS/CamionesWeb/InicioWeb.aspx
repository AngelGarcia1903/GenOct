<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InicioWeb.aspx.cs" Inherits="CamionesWeb.InicioWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="menu-page-container">

    <%-- TITULO + BOTON REFRESCAR --%>
    <div class="d-flex justify-content-between align-items-end flex-wrap gap-3 mb-4">
        <div>
            <span class="menu-subtitle">Panel</span>
            <h1 class="menu-main-title mb-0">Resumen <span>general</span></h1>
        </div>
        <div class="text-end">
            <asp:Button ID="btnRefrescar" runat="server" Text="Refrescar" CssClass="btn btn-primary rounded-pill js-cargando"
                OnClick="btnRefrescar_Click" />
            <div class="dash-actualizado mt-1">
                Actualizado: <asp:Label ID="lblActualizado" runat="server" />
            </div>
        </div>
    </div>

    <%-- MENSAJES DE ERROR --%>
    <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-danger">
        <asp:Label ID="lblMensaje" runat="server" />
    </asp:Panel>

    <%-- FILA 1: TARJETAS DE INDICADORES (row + col-md-3 = 4 tarjetas por fila) --%>
    <div class="row g-4 mb-4">

        <div class="col-md-6 col-xl-3">
            <a class="dash-card" href="Camiones">
                <div class="dash-card-label">Camiones</div>
                <asp:Label ID="lblTotalCamiones" runat="server" Text="0" CssClass="dash-card-valor" />
                <div class="dash-card-detalle">
                    <asp:Label ID="lblCamionesDisp" runat="server" Text="0" /> disponibles
                </div>
            </a>
        </div>

        <div class="col-md-6 col-xl-3">
            <a class="dash-card" href="ChoferesWeb">
                <div class="dash-card-label">Choferes</div>
                <asp:Label ID="lblTotalChoferes" runat="server" Text="0" CssClass="dash-card-valor" />
                <div class="dash-card-detalle">
                    <asp:Label ID="lblChoferesDisp" runat="server" Text="0" /> disponibles
                </div>
            </a>
        </div>

        <div class="col-md-6 col-xl-3">
            <a class="dash-card" href="RutasWeb">
                <div class="dash-card-label">Rutas</div>
                <asp:Label ID="lblTotalRutas" runat="server" Text="0" CssClass="dash-card-valor" />
                <div class="dash-card-detalle">
                    <asp:Label ID="lblRutasATiempo" runat="server" Text="0" /> llegaron a tiempo
                </div>
            </a>
        </div>

        <div class="col-md-6 col-xl-3">
            <div class="dash-card">
                <div class="dash-card-label">Kilómetros recorridos</div>
                <asp:Label ID="lblKmTotales" runat="server" Text="0" CssClass="dash-card-valor" />
                <div class="dash-card-detalle">
                    Promedio por ruta: <asp:Label ID="lblKmPromedio" runat="server" Text="0" /> km
                </div>
            </div>
        </div>

    </div>

    <%-- FILA 2: BARRAS DE PORCENTAJE (izquierda) + ULTIMAS RUTAS (derecha) --%>
    <div class="row g-4">

        <div class="col-lg-5">
            <div class="dash-panel">
                <h5 class="dash-panel-titulo">Estado de la flotilla</h5>

                <div class="dash-barra">
                    <div class="d-flex justify-content-between">
                        <span>Camiones disponibles</span>
                        <asp:Label ID="lblPctCamiones" runat="server" Text="0%" CssClass="fw-bold" />
                    </div>
                    <div class="progress">
                        <%-- el ancho (style width) se asigna desde C# --%>
                        <asp:Panel ID="barCamiones" runat="server" CssClass="progress-bar bg-success" />
                    </div>
                </div>

                <div class="dash-barra">
                    <div class="d-flex justify-content-between">
                        <span>Choferes disponibles</span>
                        <asp:Label ID="lblPctChoferes" runat="server" Text="0%" CssClass="fw-bold" />
                    </div>
                    <div class="progress">
                        <asp:Panel ID="barChoferes" runat="server" CssClass="progress-bar bg-info" />
                    </div>
                </div>

                <div class="dash-barra">
                    <div class="d-flex justify-content-between">
                        <span>Rutas a tiempo</span>
                        <asp:Label ID="lblPctATiempo" runat="server" Text="0%" CssClass="fw-bold" />
                    </div>
                    <div class="progress">
                        <asp:Panel ID="barATiempo" runat="server" CssClass="progress-bar bg-warning" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-7">
            <div class="dash-panel">
                <h5 class="dash-panel-titulo">Últimas rutas</h5>

                <%-- Repeater: repite el HTML del ItemTemplate por cada ruta de la lista --%>
                <asp:Repeater ID="rptUltimasRutas" runat="server">
                    <ItemTemplate>
                        <div class="dash-ruta">
                            <div>
                                <div class="fw-bold"><%# Eval("Origen") %> → <%# Eval("Destino") %></div>
                                <div class="dash-ruta-detalle">
                                    <%# Eval("NombreChofer") %> · Camión <%# Eval("Matricula") %> ·
                                    <%# Eval("FechaSalida", "{0:dd/MM/yyyy}") %>
                                </div>
                            </div>
                            <div class="text-end">
                                <div class="fw-bold"><%# Eval("Distancia", "{0:N1}") %> km</div>
                                <span class='badge <%# (bool)Eval("ATiempo") ? "bg-success" : "bg-danger" %>'>
                                    <%# (bool)Eval("ATiempo") ? "A tiempo" : "Con retraso" %>
                                </span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <%-- se muestra solo si no hay rutas --%>
                <asp:Label ID="lblSinRutas" runat="server" Text="Aún no hay rutas registradas." CssClass="text-muted" Visible="false" />
            </div>
        </div>

    </div>

    </div>

</asp:Content>
