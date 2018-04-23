import java.net.*;

import javax.swing.JOptionPane;

public class ControladorConcurrente {

	public static String ipRMI;
	public static Integer puertoRMI;

	/**
	 * @param args
	 */
	@SuppressWarnings("resource")
	public static void main(String[] args) {

		Integer puerto = -1;
		Integer maxPeticiones = -1;

		try{

			String aux = JOptionPane.showInputDialog("Introduce el puerto de escucha del controlador\n\n*Por defecto el 8081\n");
			if(aux==null){
				System.exit(1);
			}
			else if(aux.contentEquals("")){
				puerto = 8081;
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

			aux = JOptionPane.showInputDialog("Introduce la ip del rmi\n\n*Por defecto la del localhost\n");
			if(aux==null){
				System.exit(1);
			}
			else if (aux.contentEquals("")){
				ipRMI = "localhost";
			}
			else{
				ipRMI = aux;
			}

			aux = JOptionPane.showInputDialog("Introduce el puerto del rmi\n\n*Por defecto 1099\n");
			if(aux==null){
				System.exit(1);
			}
			else if (aux.contentEquals("")){
				puertoRMI = 1099;
			}
			else{
				puertoRMI = Integer.parseInt(aux);
			}


			ServerSocket skServidor = new ServerSocket(puerto);
			System.out.println("Escucho el puerto: " + puerto);
			System.out.println("Numero maximo de peticiones: " + maxPeticiones);
			JOptionPane.showMessageDialog(null, "Escucho el puerto: " + puerto + "\nNumero maximo de peticiones: " + maxPeticiones);

			for(;;){
				if(HiloControlador.contador < maxPeticiones){
					Socket skCliente = skServidor.accept();
					System.out.println("Sirviendo cliente...");

					Thread t = new HiloControlador(skCliente);
					t.start();
				}
			}
		}
		catch(Exception e){
			JOptionPane.showMessageDialog(null, "Error controlador: " + e.toString());
		}
	}


}
