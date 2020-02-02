using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelegramSource : MonoBehaviour {
    public static TelegramSource inst { get; private set; }

    public TextAsset lovePoemCorpus;
    public TextAsset lawBookCorpus;

    public int minChainLength = 10;
    public int maxChainLength = 30;

    private MarkovChain lovePoemChain;
    private MarkovChain lawBookChain;

    private string lovePoem;
    private string lawBook;

    private void Awake() {
        inst = this;

        MarkovChain.Config config = new MarkovChain.Config();
        config.minChainLength = this.minChainLength;
        config.maxChainLength = this.maxChainLength;
        config.random = RandomSource.rand;

        this.lovePoemChain = new MarkovChain(this.lovePoemCorpus, config);
        this.lawBookChain = new MarkovChain(this.lawBookCorpus, config);

        this.Regenerate();
    }

    public List<string> GetLovePoem() {
        return this.lovePoemChain.GetOutput();
    }

    public List<string> GetLawBook() {
        return this.lawBookChain.GetOutput();
    }

    public void Regenerate() {
        this.lovePoemChain.Regenerate();
        this.lawBookChain.Regenerate();

        Debug.Log(string.Join(" ", this.lovePoemChain.GetOutput()));
        Debug.Log(string.Join(" ", this.lawBookChain.GetOutput()));
    }
}
