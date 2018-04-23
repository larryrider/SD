import java.lang.Exception;
import java.net.Socket;
import java.io.*;
import java.util.Scanner;
import java.util.StringTokenizer;


public class HiloServidor extends Thread {

	private Socket skCliente;
	public static Integer contador = 0;
	public final String CONTROLLER = "controladorSD/";

	private final String error409 = "<HTML><HEAD><TITLE>409 Conflict</TITLE></HEAD><BODY style=\"background-color:powderblue\";><H2>No se ha podido conectar o ha ocurrido algun error con el Controlador</H2>" +
						"<p>&nbsp;</p><p>&nbsp;</p><p><a href=\"/\">Volver a la pagina principal</a></p></BODY></HTML>";
	private final String error404 = "<HTML><HEAD><TITLE>404 Not found</TITLE></HEAD><BODY style=\"background-color:powderblue\";><H2>Error 404 Not Found</H2>" + 
						"<p>&nbsp;</p><p>&nbsp;</p><p><a href=\"/\">Volver a la pagina principal</a></p></BODY></HTML>";
	private final String error405 = "<HTML><HEAD><TITLE>405 Method Not Allowed</TITLE></HEAD><BODY style=\"background-color:powderblue\";><H2>405 Method Not Allowed</H2>" + 
						"<p>&nbsp;</p><p>&nbsp;</p><p><a href=\"/\">Volver a la pagina principal</a></p></BODY></HTML>";

	public HiloServidor(Socket p_cliente) {
		this.skCliente = p_cliente;
		contador++;
	}

	private String leerCabeceraSocket() {
		String cabecera = "";

		try {
			InputStream input = skCliente.getInputStream();
			Scanner scanner = new Scanner(input);
			cabecera = scanner.nextLine();
		} catch (Exception e) {
			System.out.println("Error leyendo socket HiloServidor: " + e.toString());
		}
		return cabecera;
	}

	private void escribeSocket(String escribir) {
		try {
			PrintWriter out = new PrintWriter(skCliente.getOutputStream());
			out.print(escribir);
			out.flush();
		} catch (Exception e) {
			System.out.println("Error escribiendo socket HiloServidor: " + e.toString());
		}
	}

	private void tratarCabecera(String cabecera) {
		// GET /prueba.html HTTP/1.1
		StringTokenizer ruta = new StringTokenizer(cabecera, " ");

		if (ruta.nextToken().contentEquals("GET")) {
			String pagina = ruta.nextToken();
			pagina = pagina.substring(1);

			if(pagina.length() >= 14 && pagina.substring(0,14).contentEquals(CONTROLLER)){
				tratarControlador(pagina);
			}
			else{
				if (pagina.isEmpty()) {
					pagina = "index.html";
				}
				PaginaWeb paginaWeb = getHTML(pagina);
				if (paginaWeb.Existe()) {
					escribeSocket(construirPaqueteSocket("200", paginaWeb.getContenido()));
				} 
				else {
					escribeSocket(construirPaqueteSocket("404", error404));
				}
			}
		} 
		else {
			escribeSocket(construirPaqueteSocket("405", error405));
		}
	}

	private void tratarControlador(String pagina){
		pagina = pagina.substring(13);

		String inicioHTML = "<HTML><HEAD><TITLE>Error</TITLE></HEAD><BODY style=\"background-color:powderblue\";><H2>";
		String finalHTML = "</H2><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p><a href=\"/controladorSD/index\">Volver al controlador</a></p></BODY></HTML>";
		//String controlador = "<HTML><HEAD><TITLE>Controller</TITLE></HEAD><BODY>Conectando con el controller: "+ pagina +"</BODY></HTML>";
		//escribeSocket(construirPaqueteSocket("200", controlador));
		//System.out.println(pagina);
		if(pagina.contentEquals("/index") || pagina.contentEquals("/index.html") || pagina.contentEquals("/")){
			try{
				Socket socketCliente = new Socket (ServidorConcurrente.ipControlador, ServidorConcurrente.puertoControlador);
				DataOutputStream out = new DataOutputStream(socketCliente.getOutputStream());
				out.writeUTF("ControladorSD index");
				
				InputStream input = socketCliente.getInputStream();
				/*Scanner scanner = new Scanner(input);
				String paginaHTML = scanner.nextLine();*/
				DataInputStream data = new DataInputStream(input);
				String paginaHTML = data.readUTF();

				//System.out.println(paginaHTML);

				escribeSocket(construirPaqueteSocket("200",paginaHTML));
			}
			catch (Exception e){
				System.out.println("Error1 tratarControlador: " + e.toString());
				escribeSocket(construirPaqueteSocket("409", error409));
			}	
		}
		else{
			//System.out.println(pagina);
			try{	
				if(pagina.startsWith("/index?setluz=") && pagina.length() > 27){   //   /index?setluz=&sonda=sonda1
					pagina = pagina.substring(14);
					StringTokenizer tokenizer = new StringTokenizer(pagina, "&");
					pagina = "/setluz=" + tokenizer.nextToken() + "?";

					String tokenAux = tokenizer.nextToken();
					pagina += "sonda=" + tokenAux.substring(11);
					//System.out.println(pagina);
				}

				StringTokenizer tokens = new StringTokenizer(pagina, "?");    //   /ultimafecha?sonda=1
				String variable = tokens.nextToken().substring(1);
				String sonda = tokens.nextToken();

				if(pagina.contains("sonda") && tokens.countTokens()==0 && esValidaSonda(sonda) && esValidaVariable(variable)){
					try{
						String numSonda = sonda.substring(6);
						//System.out.println(variable);
						variable = controlarVariables(variable);

						Socket socketCliente = new Socket (ServidorConcurrente.ipControlador, ServidorConcurrente.puertoControlador);
						DataOutputStream out = new DataOutputStream(socketCliente.getOutputStream());					
						out.writeUTF("ControladorSD "+ variable + " " + numSonda);
						
						InputStream input = socketCliente.getInputStream();
						Scanner scanner = new Scanner(input);
						String paginaHTML = scanner.nextLine();
						
						//System.out.println(paginaHTML);
						escribeSocket(construirPaqueteSocket("200",paginaHTML));
					}
					catch (Exception e){
						System.out.println("Error2 tratarControlador: " + e.toString());
						//e.printStackTrace();
						
						escribeSocket(construirPaqueteSocket("409", error409));
					}
				}
				else if (!esValidaVariable(variable)){
					String error = inicioHTML + "Variable no valida: se solicita informacion de una variable de la sonda que no existe" + finalHTML;
					escribeSocket(construirPaqueteSocket("409", error));
				}
				else if (!esValidaSonda(sonda)){
					String error = inicioHTML + "La sonda no existe: se solicita informacion de una sonda que no existe" + finalHTML;
					escribeSocket(construirPaqueteSocket("409", error));
				}
				else{
					String error = inicioHTML + "Se ha detectado un error en la peticion al controlador" + finalHTML;
						
					escribeSocket(construirPaqueteSocket("409", error));
				}
			}
			catch(Exception e){
				escribeSocket(construirPaqueteSocket("409", error409));
			}
		}
	}

	private String controlarVariables(String variable){
		if (variable.startsWith("setluz=")){
			String hexColor = "";
			variable = variable.substring(7);

			//System.out.println(variable);
			try{
				Integer color = Integer.parseInt(variable);
				hexColor = String.format("#%06X", (0xFFFFFF & color));	//Integer.toHexString(color);
			}
			catch(Exception e){
				switch(variable){
					case "rojo":
						hexColor = "#FF0000";
						break;
					case "verde":
						hexColor = "#008000";
						break;
					case "ambar":
						hexColor = "#FFD700";
						break;
					case "amarillo":
						hexColor = "#FFFF00";
						break;
					case "azul":
						hexColor = "#0000FF";
						break;
					case "negro":
						hexColor = "#000000";
						break;
					case "marron":
						hexColor = "#A52A2A";
						break;
					case "gris":
						hexColor = "#808080";
						break;
					case "naranja":
						hexColor = "#FFA500";
						break;
					case "rosa":
						hexColor = "#FFC0CB";
						break;
					case "lila":
						hexColor = "#800080";
						break;
					case "blanco":
						hexColor = "#FFFFFF";
						break;	
				}
			}
			variable = "setluz=" + hexColor;
			//System.out.println(variable);
		}

		return variable;
	}

	private Boolean esValidaVariable (String variable){
		if(variable.contentEquals("volumen") || variable.contentEquals("fecha") || variable.contentEquals("ultimafecha") 
			|| variable.contentEquals("luz") || variable.startsWith("setluz=")){
			//System.out.println("Variable valida");
			return true;
		}
		else{
			return false;
		}

	}

	private Boolean esValidaSonda (String sonda){
		if(sonda.substring(0,6).contentEquals("sonda=")){
			//System.out.println("Sonda valida");
			try{
				Integer.parseInt(sonda.substring(6));
				return true;
			} catch(Exception e){
				return false;
			}
		}
		else{
			return false;
		}

	}

	private PaginaWeb getHTML(String ruta) {
		PaginaWeb pagina  = new PaginaWeb();
		String content = "";
		try {
			BufferedReader in = new BufferedReader(new FileReader(ruta));
			String str = null;
			while ((str = in.readLine()) != null) {
				content += str;
			}
			in.close();
		} catch (Exception e) {
			//e.printStackTrace();
			pagina.setExiste(false);
		}
		pagina.setContenido(content);
		return pagina;
	}
	
	class PaginaWeb{
		private Boolean existe;
		private String contenido;
		
		public PaginaWeb(){
			existe = true;
			contenido = "";
		}
		
		public Boolean Existe() {
			return existe;
		}
		public void setExiste(Boolean existe) {
			this.existe = existe;
		}
		public String getContenido() {
			return contenido;
		}
		public void setContenido(String contenidoWeb) {
			this.contenido = contenidoWeb;
		}
	}

	private String construirPaqueteSocket(String codigoEstado, String fichero) {

		String versionHTTP = "HTTP/1.1 ";

		switch (codigoEstado) {
			case "200":
				codigoEstado = "200 OK";
				break;
			case "404":
				codigoEstado = "404 Not Found";
				break;
			case "405":
				codigoEstado = "405 Method Not Allowed";
				break;
			case "409":
				codigoEstado = "409 Conflict";
				break;
		}
		codigoEstado += "\n";

		String inicio = versionHTTP + codigoEstado;

		String tipo = "Content-Type: text/html\n";
		
		String tamanyo = "Content-Length: " + fichero.length();

		String infoServidor = "Server: servidorHTTP\n";

		return inicio + tipo + infoServidor + "\n" + fichero;
	}

	public void run() {
		try {
			String cabecera = leerCabeceraSocket();
			tratarCabecera(cabecera);
			skCliente.close();
		} catch (Exception e) {
			System.out.println("Error hiloservidor: " + e.toString());
		} finally {
			contador--;
		}
	}

}

