using UnityEngine;

public class FruitCombiner : MonoBehaviour
{
    [SerializeField] private AudioClip combineSoundClip;
    [SerializeField] private GameObject combineEffectPrefab;

    private int _layerIndex;
    private FruitInfo _info;

    private void Awake()
    {
        _info = GetComponent<FruitInfo>();
        _layerIndex = gameObject.layer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layerIndex)
        {
            FruitInfo info = collision.gameObject.GetComponent<FruitInfo>();
            if (info != null)
            {
                if (info.FruitIndex == _info.FruitIndex)
                {
                    int thisID = gameObject.GetInstanceID();
                    int otherID = collision.gameObject.GetInstanceID();

                    if (thisID > otherID)
                    {
                        GameManager.instance.IncreaseScore(_info.PointsWhenAnnihilated);


                        if (_info.FruitIndex == FruitSelector.instance.Fruits.Length - 1)
                        {
                            Destroy(collision.gameObject);
                            Destroy(gameObject);
                        }

                        else
                        {
                            Vector3 middlePosition = (transform.position + collision.transform.position) / 2f;
                            GameObject go = Instantiate(SpawnCombinedFruit(_info.FruitIndex), GameManager.instance.transform);
                            go.transform.position = middlePosition;
                            go.transform.localScale = _info.transform.localScale;

                            ColliderInformer informer = go.GetComponent<ColliderInformer>();
                            if (informer != null)
                            {
                                informer.WasCombinedIn = true;
                            }

                            Destroy(collision.gameObject);
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }

    private GameObject SpawnCombinedFruit(int index)
    {

        if (combineEffectPrefab != null)
        {
            GameObject effect = Instantiate(combineEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f); // ���������� ����� 2 ������� (��� �������, ������� ������ ������)
        }

        SoundFXManager.instance.PlaySoundFXClip(combineSoundClip, transform, 1f);

        GameObject go = FruitSelector.instance.Fruits[index + 1];

        return go;
    }
}
