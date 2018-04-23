import java.net.*;

import javax.swing.JOptionPane;

public class ServidorConcurrente {

	
	public static String ipControlador;
	public static Integer puertoControlador;
	public static Integer puerto = -1;

	/**
	 * @param args
	 */
	@SuppressWarnings("resource")
	public static void main(String[] args) {
		/*
		* Descriptores de socket servidor y de socket con el cliente
		*/
		
        Integer maxPeticiones = -1;

		try{
			
			/*if (args.length < 1) {
				System.out.println("Debe indicar el puerto de escucha del servidor");
				System.out.println("$./Servidor puerto_servidor");
				System.exit(1);
			}
			puerto = args[0];*/

			String aux = JOptionPane.showInputDialog("Introduce el puerto de escucha del servidor\n\n*Por defecto el 8080\n");
			if(aux==null){
				System.exit(1);
			}
			else if(aux.contentEquals("")){
				puerto = 8080;
			}
			else{
				puerto = Integer.parseInt(aux);
			}


			aux = JOptionPane.showInputDialog("Introduce el numero maximo de peticiones simultaneas\n\n*Por defecto 10\n");
			if(aux==null){
				System.exit(1);
			}
			else if (aux.contentEquals("")){
				maxPeticiones = 10;
			}
			else{
				maxPeticiones = Integer.parseInt(aux);
			}

			aux = JOptionPane.showInputDialog("Introduce la ip del controlador\n\n*Por defecto la del localhost\n");
			if(aux==null){
				System.exit(1);
			}
			else if (aux.contentEquals("")){
				ipControlador = "localhost";
			}
			else{
				ipControlador = aux;
			}

			aux = JOptionPane.showInputDialog("Introduce el puerto del controlador\n\n*Por defecto 8081\n");
			if(aux==null){
				System.exit(1);
			}
			else if (aux.contentEquals("")){
				puertoControlador = 8081;
			}
			else{
				puertoControlador = Integer.parseInt(aux);
			}


			
			ServerSocket skServidor = new ServerSocket(puerto);
			System.out.println("Escucho el puerto: " + puerto);
			System.out.println("Numero maximo de peticiones: " + maxPeticiones);
			JOptionPane.showMessageDialog(null, "Escucho el puerto: " + puerto + "\nNumero maximo de peticiones: " + maxPeticiones);
			/*
			* Mantenemos la comunicacion con el cliente
			*/	
			for(;;){
				/*
				* Se espera un cliente que quiera conectarse
				*/

				if(HiloServidor.contador < maxPeticiones){
					Socket skCliente = skServidor.accept();
					System.out.println("Sirviendo cliente...");

					Thread t = new HiloServidor(skCliente);
					t.start();
				}
			}
		}
		catch (NumberFormatException n){
			JOptionPane.showMessageDialog(null, "Has introducido un numero incorrecto");
			main(null);
		}
		catch(Exception e){
			JOptionPane.showMessageDialog(null, "Error servidor HTTP: " + e.toString());
		}
	}


}
