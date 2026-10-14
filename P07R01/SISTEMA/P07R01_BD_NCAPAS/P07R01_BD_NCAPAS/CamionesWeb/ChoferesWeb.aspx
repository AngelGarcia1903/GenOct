<%@ Page Title="Choferes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChoferesWeb.aspx.cs" Inherits="CamionesWeb.ChoferesWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="menu-page-container">

    <%-- TITULO (clases de camionesWeb.css) --%>
    <span class="menu-subtitle">Personal</span>
    <h1 class="menu-main-title mb-4">Nuestros <span>choferes</span></h1>

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
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar chofer" CssClass="btn btn-success rounded-pill" OnClick="btnAgregar_Click" CausesValidation="false" />
    </div>

    <%-- LISTADO: camionesWeb.css convierte cada fila del GridView en una tarjeta (.row-card) --%>
    <div class="gridview-container-menu">
        <asp:GridView ID="gvChoferes" runat="server" AutoGenerateColumns="false" ShowHeader="false"
            GridLines="None" DataKeyNames="IdChofer">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="row-card">
                            <div class="row-card-image-wrapper">
                                <img class="row-card-image" src='<%# Eval("UrlFoto") %>' alt='<%# Eval("NombreCompleto") %>'
                                    onerror="this.style.display='none'" />
                                <span class='badge-floating <%# (bool)Eval("Disponibilidad") ? "badge-status-disponible" : "badge-status-no-disponible" %>'>
                                    <%# (bool)Eval("Disponibilidad") ? "Disponible" : "No disponible" %>
                                </span>
                            </div>
                            <div class="row-card-body">
                                <div class="row-card-category">Licencia <%# Eval("Licencia") %></div>
                                <div class="row-card-title"><%# Eval("NombreCompleto") %></div>
                                <div class="row-card-description">
                                    Teléfono: <%# Eval("Telefono") %><br />
                                    Nacimiento: <%# Eval("FechaNacimiento", "{0:dd/MM/yyyy}") %>
                                </div>
                                <div class="row-card-footer">
                                    <div class="row-card-price-area">
                                        <span class="row-card-price">#<%# Eval("IdChofer") %> <span>ID</span></span>
                                    </div>
                                    <div>
                                        <asp:LinkButton ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-sm btn-warning"
                                            CommandArgument='<%# Eval("IdChofer") %>' OnClick="btnEditar_Click" CausesValidation="false" />
                                        <asp:LinkButton ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger"
                                            CommandArgument='<%# Eval("IdChofer") %>' OnClick="btnEliminar_Click" CausesValidation="false" />
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

    <%-- ID del chofer que se edita o elimina (0 = nuevo) --%>
    <asp:HiddenField ID="hfIdChofer" runat="server" Value="0" />

    <%-- MODAL FORMULARIO (lo abre/cierra mostrarModalFormulario en camionesWeb.js) --%>
    <div class="modal fade" id="modalFormulario" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Datos del chofer</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-2">
                        <label>Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="100" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                            ErrorMessage="El nombre es obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Chofer" />
                    </div>
                    <div class="mb-2">
                        <label>Apellido paterno</label>
                        <asp:TextBox ID="txtApPaterno" runat="server" CssClass="form-control" MaxLength="100" />
                        <asp:RequiredFieldValidator ID="rfvApPaterno" runat="server" ControlToValidate="txtApPaterno"
                            ErrorMessage="El apellido paterno es obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Chofer" />
                    </div>
                    <div class="mb-2">
                        <label>Apellido materno</label>
                        <asp:TextBox ID="txtApMaterno" runat="server" CssClass="form-control" MaxLength="100" />
                        <asp:RequiredFieldValidator ID="rfvApMaterno" runat="server" ControlToValidate="txtApMaterno"
                            ErrorMessage="El apellido materno es obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Chofer" />
                    </div>
                    <div class="mb-2">
                        <label>Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" MaxLength="10" />
                        <asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtTelefono"
                            ValidationExpression="^\d{10}$" ErrorMessage="El teléfono debe tener 10 dígitos"
                            CssClass="text-danger" Display="Dynamic" ValidationGroup="Chofer" />
                        <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono"
                            ErrorMessage="El teléfono es obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Chofer" />
                    </div>
                    <div class="mb-2">
                        <label>Fecha de nacimiento</label>
                        <%-- el max (mayor de 18) se asigna en InicializarControles() --%>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date" />
                        <asp:RequiredFieldValidator ID="rfvFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento"
                            ErrorMessage="La fecha de nacimiento es obligatoria" CssClass="text-danger" Display="Dynamic" ValidationGroup="Chofer" />
                    </div>
                    <div class="mb-2">
                        <label>Licencia</label>
                        <asp:TextBox ID="txtLicencia" runat="server" CssClass="form-control" MaxLength="50" />
                        <asp:RequiredFieldValidator ID="rfvLicencia" runat="server" ControlToValidate="txtLicencia"
                            ErrorMessage="La licencia es obligatoria" CssClass="text-danger" Display="Dynamic" ValidationGroup="Chofer" />
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
                        OnClick="btnGuardar_Click" ValidationGroup="Chofer" />
                </div>
            </div>
        </div>
    </div>

    <%-- MODAL ELIMINAR (lo abre/cierra mostrarModalEliminar en camionesWeb.js) --%>
    <div class="modal fade" id="modalEliminar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar chofer</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    ¿Seguro que desea eliminar este chofer?
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
