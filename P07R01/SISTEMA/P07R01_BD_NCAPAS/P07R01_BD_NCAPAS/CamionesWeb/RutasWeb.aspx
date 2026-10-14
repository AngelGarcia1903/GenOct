<%@ Page Title="Rutas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RutasWeb.aspx.cs" Inherits="CamionesWeb.RutasWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="menu-page-container">

    <%-- TITULO (clases de camionesWeb.css) --%>
    <span class="menu-subtitle">Viajes</span>
    <h1 class="menu-main-title mb-4">Nuestras <span>rutas</span></h1>

    <%-- MENSAJES --%>
    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
        <asp:Label ID="lblMensaje" runat="server" />
        <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn-close" OnClick="btnCerrar_Click" CausesValidation="false" />
    </asp:Panel>

    <%-- ACCIONES (N_Ruta.ListarRutas no tiene filtro, por eso no hay DropDownList de filtro) --%>
    <div class="filtros">
        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-outline-secondary rounded-pill js-cargando" OnClick="btnActualizar_Click" CausesValidation="false" />
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar ruta" CssClass="btn btn-success rounded-pill" OnClick="btnAgregar_Click" CausesValidation="false" />
    </div>

    <%-- LISTADO: camionesWeb.css convierte cada fila del GridView en una tarjeta (.row-card) --%>
    <div class="gridview-container-menu">
        <asp:GridView ID="gvRutas" runat="server" AutoGenerateColumns="false" ShowHeader="false"
            GridLines="None" DataKeyNames="IdRuta">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="row-card">
                            <div class="row-card-image-wrapper">
                                <img class="row-card-image" src='<%# Eval("FotoCamion") %>' alt='<%# Eval("Matricula") %>'
                                    onerror="this.style.display='none'" />
                                <span class='badge-floating <%# (bool)Eval("ATiempo") ? "badge-status-disponible" : "badge-status-no-disponible" %>'>
                                    <%# (bool)Eval("ATiempo") ? "A tiempo" : "Con retraso" %>
                                </span>
                            </div>
                            <div class="row-card-body">
                                <div class="row-card-category">Camión <%# Eval("Matricula") %></div>
                                <div class="row-card-title"><%# Eval("Origen") %> → <%# Eval("Destino") %></div>
                                <div class="row-card-description">
                                    Chofer: <%# Eval("NombreChofer") %> (Lic. <%# Eval("Licencia") %>)<br />
                                    Salida: <%# Eval("FechaSalida", "{0:dd/MM/yyyy HH:mm}") %><br />
                                    Llegada: <%# Eval("FechaLlegada", "{0:dd/MM/yyyy HH:mm}") %>
                                </div>
                                <div class="row-card-footer">
                                    <div class="row-card-price-area">
                                        <span class="row-card-price"><%# Eval("Distancia", "{0:N1}") %> <span>km</span></span>
                                        <span class="row-card-subtext">Ruta #<%# Eval("IdRuta") %></span>
                                    </div>
                                    <div>
                                        <asp:LinkButton ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-sm btn-warning"
                                            CommandArgument='<%# Eval("IdRuta") %>' OnClick="btnEditar_Click" CausesValidation="false" />
                                        <asp:LinkButton ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger"
                                            CommandArgument='<%# Eval("IdRuta") %>' OnClick="btnEliminar_Click" CausesValidation="false" />
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

    <%-- ID de la ruta que se edita o elimina (0 = nueva) --%>
    <asp:HiddenField ID="hfIdRuta" runat="server" Value="0" />

    <%-- MODAL FORMULARIO (lo abre/cierra mostrarModalFormulario en camionesWeb.js) --%>
    <div class="modal fade" id="modalFormulario" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Datos de la ruta</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-2">
                        <label>Chofer</label>
                        <%-- se llena desde N_Chofer en CargarCombos() --%>
                        <asp:DropDownList ID="ddlChofer" runat="server" CssClass="form-select" />
                        <asp:RequiredFieldValidator ID="rfvChofer" runat="server" ControlToValidate="ddlChofer" InitialValue="0"
                            ErrorMessage="Seleccione un chofer" CssClass="text-danger" Display="Dynamic" ValidationGroup="Ruta" />
                    </div>
                    <div class="mb-2">
                        <label>Camión</label>
                        <%-- se llena desde N_Camion en CargarCombos() --%>
                        <asp:DropDownList ID="ddlCamion" runat="server" CssClass="form-select" />
                        <asp:RequiredFieldValidator ID="rfvCamion" runat="server" ControlToValidate="ddlCamion" InitialValue="0"
                            ErrorMessage="Seleccione un camión" CssClass="text-danger" Display="Dynamic" ValidationGroup="Ruta" />
                    </div>
                    <div class="mb-2">
                        <label>Origen</label>
                        <asp:TextBox ID="txtOrigen" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvOrigen" runat="server" ControlToValidate="txtOrigen"
                            ErrorMessage="El origen es obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Ruta" />
                    </div>
                    <div class="mb-2">
                        <label>Destino</label>
                        <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvDestino" runat="server" ControlToValidate="txtDestino"
                            ErrorMessage="El destino es obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Ruta" />
                    </div>
                    <div class="mb-2">
                        <label>Fecha y hora de salida</label>
                        <asp:TextBox ID="txtFechaSalida" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />
                        <asp:RequiredFieldValidator ID="rfvFechaSalida" runat="server" ControlToValidate="txtFechaSalida"
                            ErrorMessage="La fecha de salida es obligatoria" CssClass="text-danger" Display="Dynamic" ValidationGroup="Ruta" />
                    </div>
                    <div class="mb-2">
                        <label>Fecha y hora de llegada</label>
                        <asp:TextBox ID="txtFechaLlegada" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />
                        <asp:RequiredFieldValidator ID="rfvFechaLlegada" runat="server" ControlToValidate="txtFechaLlegada"
                            ErrorMessage="La fecha de llegada es obligatoria" CssClass="text-danger" Display="Dynamic" ValidationGroup="Ruta" />
                    </div>
                    <div class="mb-2">
                        <label>Distancia (km)</label>
                        <asp:TextBox ID="txtDistancia" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvDistancia" runat="server" ControlToValidate="txtDistancia"
                            ErrorMessage="La distancia es obligatoria" CssClass="text-danger" Display="Dynamic" ValidationGroup="Ruta" />
                        <asp:RangeValidator ID="rvDistancia" runat="server" ControlToValidate="txtDistancia" Type="Double"
                            MinimumValue="0.1" MaximumValue="100000" ErrorMessage="La distancia debe ser mayor a 0"
                            CssClass="text-danger" Display="Dynamic" ValidationGroup="Ruta" />
                    </div>
                    <div class="form-check">
                        <asp:CheckBox ID="chkATiempo" runat="server" />
                        <label class="form-check-label">Llegó a tiempo</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary"
                        OnClick="btnGuardar_Click" ValidationGroup="Ruta" />
                </div>
            </div>
        </div>
    </div>

    <%-- MODAL ELIMINAR (lo abre/cierra mostrarModalEliminar en camionesWeb.js) --%>
    <div class="modal fade" id="modalEliminar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar ruta</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    ¿Seguro que desea eliminar esta ruta?
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
