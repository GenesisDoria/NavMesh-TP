using UnityEngine;
using System.Collections;

public class Persecucion : MonoBehaviour {

	public Color ColorEstado = Color.red;

	private MaquinadeEstados maquinaDeEstados;
	private ControladorNavMesh controladorNavMesh;
	private ControladorVison controladorVision;

	void Awake () {
		maquinaDeEstados = GetComponent<MaquinadeEstados>();
		controladorNavMesh = GetComponent<ControladorNavMesh>();
		controladorVision = GetComponent<ControladorVison>();
	}

	void OnEnable()
	{
		maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
	}

	void Update () {
		RaycastHit hit;
		if(!controladorVision.PuedeVerAlJugador(out hit, true))
		{
			maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
			return;
		}

		controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();    
	}
}
