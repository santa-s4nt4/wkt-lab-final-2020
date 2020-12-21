using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(AudioSource), typeof(VisualEffect))]
public class VfxSoundSample : MonoBehaviour {
    AudioSource audioSource;
    VisualEffect visualEffect;

    // 波形データ長
    // AudioSource.GetOutputDataから2の累乗である必要がある
    int outputLength = 256;

    // スペクトラムデータ長
    // AudioSource.GetSpectrumDataから2の累乗である必要がある
    int spectrumLength = 256;

    // 波形データを格納する配列
    float[] outputSamples;
    // スペクトラムデータを格納する配列
    float[] spectrumSamples;

    // VFX Graphに渡す波形データを格納するテクスチャ
    Texture2D outputMap;
    // VFX Graphに渡すスペクトラムデータを格納するテクスチャ
    Texture2D spectrumMap;

    // 波形データのテクスチャ更新時に使用する配列
    Color[] outputColors;
    // スペクトラムデータのテクスチャ更新時に使用する配列
    Color[] spectrumColors;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        visualEffect = GetComponent<VisualEffect>();

        // 配列の初期化
        outputSamples = new float[outputLength];
        spectrumSamples = new float[spectrumLength];
        outputColors = new Color[outputLength];
        spectrumColors = new Color[spectrumLength];

        // 波形データのテクスチャの作成
        outputMap = new Texture2D(outputLength, 1, TextureFormat.RFloat, false);
        outputMap.filterMode = FilterMode.Bilinear;
        outputMap.wrapMode = TextureWrapMode.Clamp;
        // スペクトラムデータのテクスチャの作成
        spectrumMap = new Texture2D(spectrumLength, 1, TextureFormat.RFloat, false);
        spectrumMap.filterMode = FilterMode.Bilinear;
        spectrumMap.wrapMode = TextureWrapMode.Clamp;
        // 波形データのテクスチャをVFX Graphの「Output Map」というプロパティに設定
        visualEffect.SetTexture("Output Map", outputMap);
        // スペクトラムデータのテクスチャをVFX Graphの「Spectrum Map」というプロパティに設定
        visualEffect.SetTexture("Spectrum Map", spectrumMap);

        // AudioSourceにマイクを設定する
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { } // 開始後すぐにマイク入力が取得できるように待つ
        audioSource.Play();
    }

    void Update() {
        // 波形データの取得
        audioSource.GetOutputData(outputSamples, 0);
        // スペクトラムデータの取得
        audioSource.GetSpectrumData(spectrumSamples, 0, FFTWindow.Hamming);

        // 波形データのテクスチャを更新
        for (var i = 0; i < outputLength; i++) {
            outputColors[i].r = outputSamples[i];
        }
        outputMap.SetPixels(outputColors);
        outputMap.Apply();

        // スペクトラムデータのテクスチャを更新
        for (var i = 0; i < spectrumLength; i++) {
            spectrumColors[i].r = spectrumSamples[i];
        }
        spectrumMap.SetPixels(spectrumColors);
        spectrumMap.Apply();

    }
}