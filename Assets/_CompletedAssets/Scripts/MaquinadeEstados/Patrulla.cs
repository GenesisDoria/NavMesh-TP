using UnityEngine;
using System.Collections;

public class Patrulla : MonoBehaviour {
	public Transform[] WayPoints;
	public Color ColorEstado = Color.green;

	private MaquinadeEstados maquinaDeEstados;
	private ControladorNavMesh controladorNavMesh;
	private ControladorVison controladorVision;
	private int siguienteWayPoint;

	void Awake()
	{
		maquinaDeEstados = GetComponent<MaquinadeEstados>();
		controladorNavMesh = GetComponent<ControladorNavMesh>();
		controladorVision = GetComponent<ControladorVison>();
	}

	// Update is called once per frame
	void Update () {
		// Ve al jugador?
		RaycastHit hit;
		if(controladorVision.PuedeVerAlJugador(out hit))
		{
			controladorNavMesh.perseguirObjectivo = hit.transform;
			maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecucion);
			return;
		}

		if (controladorNavMesh.HemosLlegado())
		{
			siguienteWayPoint = (siguienteWayPoint + 1) % WayPoints.Length;
			ActualizarWayPointDestino();
		}
	}

	void OnEnable()
	{
		maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
		ActualizarWayPointDestino();
	}

	void ActualizarWayPointDestino()
	{
		controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent(WayPoints[siguienteWayPoint].position);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && enabled)
		{
			maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
		}
	}
}
