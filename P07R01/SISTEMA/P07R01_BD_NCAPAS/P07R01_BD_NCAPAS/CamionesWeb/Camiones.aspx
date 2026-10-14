<%@ Page Title="Camiones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Camiones.aspx.cs" Inherits="CamionesWeb.Camiones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="menu-page-container">

    <%-- TITULO (clases de camionesWeb.css) --%>
    <span class="menu-subtitle">Flotilla</span>
    <h1 class="menu-main-title mb-4">Nuestros <span>camiones</span></h1>

    <%-- MENSAJES --%>
    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
        <asp:Label ID="lblMensaje" runat="server" />
        <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn-close" OnClick="btnCerrar_Click" CausesValidation="false" />
    </asp:Panel>

    <%-- FILTROS Y ACCIONES --%>
    <div class="filtros">
        <label for="<%= ddFiltro.ClientID %>">Disponibilidad:</label>
        <asp:DropDownList ID="ddFiltro" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnFiltrar_Click">
            <asp:ListItem Value="0" Text="Todos" />
            <asp:ListItem Value="1" Text="Disponibles" />
            <asp:ListItem Value="2" Text="No disponibles" />
        </asp:DropDownList>
        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary rounded-pill js-cargando" OnClick="btnFiltrar_Click" CausesValidation="false" />
        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-outline-secondary rounded-pill js-cargando" OnClick="btnActualizar_Click" CausesValidation="false" />
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar camión" CssClass="btn btn-success rounded-pill" OnClick="btnAgregar_Click" CausesValidation="false" />
    </div>

    <%-- LISTADO: camionesWeb.css convierte cada fila del GridView en una tarjeta (.row-card) --%>
    <div class="gridview-container-menu">
        <asp:GridView ID="gvCamiones" runat="server" AutoGenerateColumns="false" ShowHeader="false"
            GridLines="None" DataKeyNames="IdCamion">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="row-card">
                            <div class="row-card-image-wrapper">
                                <img class="row-card-image" src='<%# Eval("UrlFoto") %>' alt='<%# Eval("Matricula") %>'
                                    onerror="this.style.display='none'" />
                                <span class='badge-floating <%# (bool)Eval("Disponibilidad") ? "badge-status-disponible" : "badge-status-no-disponible" %>'>
                                    <%# (bool)Eval("Disponibilidad") ? "Disponible" : "No disponible" %>
                                </span>
                            </div>
                            <div class="row-card-body">
                                <div class="row-card-category"><%# Eval("TipoCamion") %></div>
                                <div class="row-card-title"><%# Eval("Marca") %> <%# Eval("Modelo") %></div>
                                <div class="row-card-description">
                                    Matrícula: <%# Eval("Matricula") %><br />
                                    Capacidad: <%# Eval("Capacidad") %> · Kilometraje: <%# Eval("Kilometraje", "{0:N0}") %> km
                                </div>
                                <div class="row-card-footer">
                                    <div class="row-card-price-area">
                                        <span class="row-card-price">#<%# Eval("IdCamion") %> <span>ID</span></span>
                                    </div>
                                    <div>
                                        <asp:LinkButton ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-sm btn-warning"
                                            CommandArgument='<%# Eval("IdCamion") %>' OnClick="btnEditar_Click" CausesValidation="false" />
                                        <asp:LinkButton ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger"
                                            CommandArgument='<%# Eval("IdCamion") %>' OnClick="btnEliminar_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    </div>

    <%-- ID del camión que se edita o elimina (0 = nuevo) --%>
    <asp:HiddenField ID="hfIdCamion" runat="server" Value="0" />

    <%-- MODAL FORMULARIO (lo abre/cierra mostrarModalFormulario en camionesWeb.js) --%>
    <div class="modal fade" id="modalFormulario" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Datos del camión</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-2">
                        <label>Matrícula</label>
                        <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" MaxLength="20" />
                        <asp:RequiredFieldValidator ID="rfvMatricula" runat="server" ControlToValidate="txtMatricula"
                            ErrorMessage="La matrícula es obligatoria" CssClass="text-danger" Display="Dynamic" ValidationGroup="Camion" />
                    </div>
                    <div class="mb-2">
                        <label>Tipo de camión</label>
                        <asp:DropDownList ID="ddlTipoCamion" runat="server" CssClass="form-select">
                            <asp:ListItem Value="" Text="-- Seleccione --" />
                            <asp:ListItem Value="CAJA SECA" Text="Caja seca" />
                            <asp:ListItem Value="TOLVA" Text="Tolva" />
                            <asp:ListItem Value="PIPA" Text="Pipa" />
                            <asp:ListItem Value="REFRIGERADO" Text="Refrigerado" />
                            <asp:ListItem Value="TORTON" Text="Torton" />
                            <asp:ListItem Value="CAMIONETA" Text="Camioneta" />






                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvTipoCamion" runat="server" ControlToValidate="ddlTipoCamion" InitialValue=""
                            ErrorMessage="Seleccione el tipo de camión" CssClass="text-danger" Display="Dynamic" ValidationGroup="Camion" />
                    </div>
                    <div class="mb-2">
                        <label>Marca</label>
                        <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvMarca" runat="server" ControlToValidate="txtMarca"
                            ErrorMessage="La marca es obligatoria" CssClass="text-danger" Display="Dynamic" ValidationGroup="Camion" />
                    </div>
                    <div class="mb-2">
                        <label>Modelo (año)</label>
                        <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control" TextMode="Number" />
                        <asp:RequiredFieldValidator ID="rfvModelo" runat="server" ControlToValidate="txtModelo"
                            ErrorMessage="El modelo es obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Camion" />
                        <%-- MaximumValue se asigna en InicializarControles() --%>
                        <asp:RangeValidator ID="rvModelo" runat="server" ControlToValidate="txtModelo" Type="Integer"
                            MinimumValue="1900" MaximumValue="2100" CssClass="text-danger" Display="Dynamic" ValidationGroup="Camion" />
                    </div>
                    <div class="mb-2">
                        <label>Capacidad</label>
                        <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control" TextMode="Number" />
                        <asp:RequiredFieldValidator ID="rfvCapacidad" runat="server" ControlToValidate="txtCapacidad"
                            ErrorMessage="La capacidad es obligatoria" CssClass="text-danger" Display="Dynamic" ValidationGroup="Camion" />
                    </div>
                    <div class="mb-2">
                        <label>Kilometraje</label>
                        <asp:TextBox ID="txtKilometraje" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvKilometraje" runat="server" ControlToValidate="txtKilometraje"
                            ErrorMessage="El kilometraje es obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Camion" />
                    </div>
                    <div class="mb-2">
                        <label>URL de la foto</label>
                        <asp:TextBox ID="txtUrlFoto" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-check">
                        <asp:CheckBox ID="chkDisponible" runat="server" />
                        <label class="form-check-label">Disponible</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary"
                        OnClick="btnGuardar_Click" ValidationGroup="Camion" />
                </div>
            </div>
        </div>
    </div>

    <%-- MODAL ELIMINAR (lo abre/cierra mostrarModalEliminar en camionesWeb.js) --%>
    <div class="modal fade" id="modalEliminar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar camión</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    ¿Seguro que desea eliminar este camión?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                        OnClick="btnConfirmarEliminar_Click" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
