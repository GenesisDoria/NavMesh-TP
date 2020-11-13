using UnityEngine;
using System.Collections;

public class Alerta : MonoBehaviour {

	public float velocidadGiroBusqueda = 120f;
	public float duracionBusqueda = 5f;
	public Color ColorEstado = Color.yellow;

	private MaquinadeEstados maquinaDeEstados;
	private ControladorNavMesh controladorNavMesh;
	private ControladorVison controladorVision;
	private float tiempoBuscando;

	void Awake () {
		maquinaDeEstados = GetComponent<MaquinadeEstados>();
		controladorNavMesh = GetComponent<ControladorNavMesh>();
		controladorVision = GetComponent<ControladorVison>();
	}

	void OnEnable()
	{
		maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
		controladorNavMesh.DetenerNavMeshAgent();
		tiempoBuscando = 0f;
	}

	void Update () {
		RaycastHit hit;
		if (controladorVision.PuedeVerAlJugador(out hit))
		{
			controladorNavMesh.perseguirObjectivo = hit.transform;
			maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecucion);
			return;
		}

		transform.Rotate(0f, velocidadGiroBusqueda * Time.deltaTime, 0f);
		tiempoBuscando += Time.deltaTime;
		if(tiempoBuscando >= duracionBusqueda)
		{
			maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPatrulla);
			return;
		}
	}
}
