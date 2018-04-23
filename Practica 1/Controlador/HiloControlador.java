import java.lang.Exception;
import java.net.Socket;
import java.io.*;
import java.util.Scanner;
import java.util.StringTokenizer;
import java.rmi.*;
import java.net.*;
import java.awt.Color; 

@SuppressWarnings("deprecation")
public class HiloControlador extends Thread {

	private Socket skCliente;
	public static Integer contador = 0;

	public HiloControlador(Socket p_cliente) {
		this.skCliente = p_cliente;
		contador++;
	}

	private String leerCabeceraSocket() {
		String cabecera = "";

		try {
			InputStream input = skCliente.getInputStream();
			DataInputStream flujo = new DataInputStream(input);
			cabecera = flujo.readUTF();
		} catch (Exception e) {
			System.out.println("Error leyendo socket HiloControlador: " + e.toString());
		}
		return cabecera;
	}

	private void escribeSocket(String escribir) {
		try {

			PrintWriter out = new PrintWriter(skCliente.getOutputStream());
			out.println(escribir);
			out.flush();
		} catch (Exception e) {
			System.out.println("Error escribiendo socket HiloControlador: " + e.toString());
		}
	}

	private void tratarCabecera(String cabecera) {
		// GET /prueba.html HTTP/1.1
		StringTokenizer ruta = new StringTokenizer(cabecera, " ");

		String peticion = ruta.nextToken();

		String inicioHTML = "<HTML><HEAD><TITLE>Controller</TITLE></HEAD><BODY>";
		String finalHTML = "<p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p><a href=\"/controladorSD/index\">Volver a la pagina principal del Controlador</a></p></BODY></HTML>\n";

		//System.out.println(cabecera);
		try{
			if(peticion.contentEquals("ControladorSD")){
				String variable = ruta.nextToken();
				String servidor = "rmi://"+ControladorConcurrente.ipRMI + ":" + ControladorConcurrente.puertoRMI;

				if (variable.contentEquals("index")){
					String sondas = "";
					for(int i=1; i <= Naming.list(servidor).length; i++){
						String sonda = servidor + "/ObjetoRemoto"+i;
						
						//System.setSecurityManager(new RMISecurityManager());
						System.setSecurityManager(new SecurityManager());
						InterfazRemoto objetoRemoto = (InterfazRemoto) Naming.lookup(sonda);

						String imagen = "<img id=\"estado\" width=\"175\" height=\"175\" src=\"http://icon-icons.com/icons2/37/PNG/512/infraredsensor_sensor_3234.png\">";

						sondas+="<table align=\"center\"><td><form method=\"get\" action=\"#\" id=\"my_form\"><fieldset>" +
							"<legend>Sonda " + i +"</legend><ol>" +
							"<li><a href=\"/controladorSD/volumen?sonda=" + i + "\">Volumen</a></li>" +
							"<li><a href=\"/controladorSD/fecha?sonda=" + i +  "\">Fecha</a></li>" +
							"<li><a href=\"/controladorSD/ultimafecha?sonda=" + i +  "\">Ultima Fecha</a></li>" +
							"<li><a href=\"/controladorSD/luz?sonda=" + i +  "\">Luz</a></li>" +
							"<li>Modificar luz: " +
							"<input type=\"text\" name=\"setluz\" id=\"setluz\" >" +
							"<input type=\"submit\" name=\"sonda\" value=\"sonda" + i +"\"></li>" +
							"</ol></fieldset></form></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>" +
							"<td bgcolor=\"" + objetoRemoto.getLuz() +"\">" + imagen + "</td> " +
							"</table>";
					}
					//System.out.println(estaciones);
					//escribeSocket(inicioHTML +estaciones+ finalHTML);
					OutputStream out = skCliente.getOutputStream();
					DataOutputStream data = new DataOutputStream(out);
					data.writeUTF(inicioHTML + sondas + "<p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><a href=\"/\">Volver al servidor HTTP</a></BODY></HTML>\n");
				}
				else{
					Integer numSonda= Integer.parseInt(ruta.nextToken());

					if (numSonda > Naming.list(servidor).length || numSonda <= 0){
						String error = inicioHTML + "<H2>La sonda no existe: se solicita informacion de una sonda que no existe</H2>" + finalHTML;
						escribeSocket(error);
					}
					else{
						String sonda = servidor + "/ObjetoRemoto" + numSonda;
						
						System.setSecurityManager(new RMISecurityManager());
						InterfazRemoto objetoRemoto = (InterfazRemoto) Naming.lookup(sonda);

						if(variable.contentEquals("volumen")){
							escribeSocket(inicioHTML + "El volumen de la sonda " + numSonda +" es: " + objetoRemoto.getVolumen().toString() + finalHTML);
						}
						else if (variable.contentEquals("fecha")){
							escribeSocket(inicioHTML +"La fecha de la sonda " + numSonda +" es: " + objetoRemoto.getFecha().toString() + finalHTML);
						}
						else if (variable.contentEquals("ultimafecha")){
							escribeSocket(inicioHTML +"La ultima fecha de la sonda " + numSonda +" es: " + objetoRemoto.getUltimaFecha().toString() + finalHTML);
						}
						else if (variable.contentEquals("luz")){
							escribeSocket(inicioHTML +"La luz de la sonda " + numSonda +" es: HEX(" + objetoRemoto.getLuz() + "), RGB"+ hex2Rgb(objetoRemoto.getLuz()) + finalHTML);
						}
						else if (variable.startsWith("setluz=")){
							objetoRemoto.setLuz(URLDecoder.decode(variable.substring(7),"UTF-8"));
							escribeSocket(inicioHTML +"La luz de la sonda " + numSonda + " ha sido modificada con el color: HEX(" + URLDecoder.decode(variable.substring(7),"UTF-8") + 
								"), RGB("+ hex2Rgb(URLDecoder.decode(variable.substring(7),"UTF-8"))+ ")" + finalHTML);
						}
					}
				}
			}
		}
		catch (Exception e){
			System.out.println("Error en tratarCabecera Controlador: " + e.toString());
			//e.printStackTrace();
		}
	}

	public static String hex2Rgb(String colorStr) {
	    Color c = new Color(
	        Integer.valueOf(colorStr.substring(1, 3), 16), 
	        Integer.valueOf(colorStr.substring(3, 5), 16), 
	        Integer.valueOf(colorStr.substring(5, 7), 16));

	    StringBuffer sb = new StringBuffer();
	    sb.append("(");
	    sb.append(c.getRed());
	    sb.append(",");
	    sb.append(c.getGreen());
	    sb.append(",");
	    sb.append(c.getBlue());
	    sb.append(")");
	    return sb.toString();
}

	public void run() {
		try {
			String cabecera = leerCabeceraSocket();
			tratarCabecera(cabecera);
			skCliente.close();
		} catch (Exception e) {
			System.out.println("Error HiloControlador: " + e.toString());
		} finally {
			contador--;
		}
	}

}

