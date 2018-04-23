import java.io.*; 
import java.rmi.*;
import java.rmi.server.*;
import java.text.SimpleDateFormat;
import java.util.*;
import java.time.*;
import java.time.format.DateTimeFormatter;


public class ObjetoRemoto extends UnicastRemoteObject implements InterfazRemoto, Serializable {     
	
    /**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private Integer volumen;
    private String fecha;
    private String ultimaFecha;
    private String luz;

    private Integer identificador;

    public ObjetoRemoto(Integer id) throws RemoteException {
		super();
        identificador = id;

        rellenarVariables();
        rellenarFichero();
	}

    @Override
    public Integer getVolumen(){
        rellenarVariables();
        return volumen;
    }
    
	@Override
	public String getFecha() throws RemoteException {
		rellenarVariables();
        return fecha;
	}

	@Override
	public String getUltimaFecha() throws RemoteException {
		rellenarVariables();
        return ultimaFecha;
	}

	@Override
	public String getLuz() throws RemoteException {
		rellenarVariables();
        return luz;
	}

	@Override
	public void setLuz(String luzNueva) throws RemoteException {
		rellenarVariables();
		luz = luzNueva;
		rellenarFichero();
	}

    private void rellenarFichero(){
        try{
            File fichero = new File("./sonda" + identificador + ".txt");
            BufferedWriter buffer = new BufferedWriter(new FileWriter(fichero));

            String texto = "volumen=" + volumen + "\nultimaFecha=" + getFechaActual() + "\nluz=" + luz;
            buffer.write(texto);
            buffer.close();
        } catch(FileNotFoundException nf){
        	System.out.println("Creando fichero: sonda" + identificador + ".txt");
        } catch(Exception e){
            System.out.println("Error en objetoRemoto: " + e.toString());
        }
    }

    private void rellenarVariables(){
        try{
            fecha = getFechaActual();
            File fichero = new File("./sonda" + identificador + ".txt");
            BufferedReader buffer = new BufferedReader(new FileReader(fichero));
            String linea = buffer.readLine();
            while(linea!=null){
                StringTokenizer variable = new StringTokenizer(linea, "=");
                variable.nextToken();
                if(variable.countTokens()!=0){
                    if(linea.contains("volumen")){
                        volumen = Integer.parseInt(variable.nextToken());
                    }
                    else if(linea.contains("ultimaFecha")){
                        ultimaFecha = variable.nextToken();
                    }
                    else if(linea.contains("luz")){
                        luz = variable.nextToken();
                    }
                }
                linea = buffer.readLine();
            }
            buffer.close();
        } catch(FileNotFoundException nf){
        	System.out.println("Creando fichero: sonda" + identificador + ".txt");
        } catch(Exception e){
            System.out.println("Error en objetoRemoto: " + e.toString());
        }
        valoresDefecto();
    }

    private void valoresDefecto(){
        if(volumen == null){
            volumen = 0;
        }
        if(fecha == null){
        	fecha = getFechaActual();
        }
        if(ultimaFecha == null){
      	   	ultimaFecha = "10/10/2010 10:10:10";
        }
        if(luz == null){
            luz = "#000000";
        }
    }
    
    private String getFechaActual(){
    	LocalDateTime currentTime = LocalDateTime.now();
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm:ss");

        return currentTime.format(formatter);
    }
}
