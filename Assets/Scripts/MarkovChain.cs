using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MarkovChain {
    public sealed class Config {
        public int minChainLength { get; set; }
        public int maxChainLength { get; set; }
        public System.Random random { get; set; }
    }

    private TextAsset source;
    private Config config;

    private Dictionary<Tuple<string, string>, List<string>> edges = new Dictionary<Tuple<string, string>, List<string>>();
    private List<string> output = new List<string>();

    public MarkovChain(TextAsset source, Config config) {
        this.source = source;
        this.config = config;

        this.edges = GenerateEdges(this.source, config);
    }

    public void Regenerate() {
        this.output = ChainEdges(this.edges, this.config);
    }

    public List<string> GetOutput() {
        return this.output;
    }

    private static Dictionary<Tuple<string, string>, List<string>> GenerateEdges(TextAsset source, Config config) {
        Dictionary<Tuple<string, string>, List<string>> edges = new Dictionary<Tuple<string, string>, List<string>>();

        List<string> words = new List<string>();
        string[] rawWords = source.text.Split(' ');
        for (int i = 0; i < rawWords.Length; i++) {
            if (!string.IsNullOrWhiteSpace(rawWords[i])) {
                words.Add(rawWords[i]);
            }
        }

        for (int i = 0; i < words.Count - 3; i++) {
            Tuple<string, string> key = new Tuple<string, string>(words[i], words[i + 1]);
            string value = words[i + 2];
            if (edges.ContainsKey(key)) {
                edges[key].Add(value);
            } else {
                List<string> valueList = new List<string>();
                valueList.Add(value);
                edges.Add(key, valueList);
            }
        }

        Debug.Log("Created " + edges.Count + " edges.");
        return edges;
    }

    private static List<string> ChainEdges(Dictionary<Tuple<string, string>, List<string>> edges, Config config) {
        Tuple<string, string> edge = new List<Tuple<string, string>>(edges.Keys)[config.random.Next(edges.Count)];

        List<string> chain = new List<string>();

        chain.Add(edge.first);
        chain.Add(edge.second);

        for (int i = 0; i < config.maxChainLength; i++) {
            List<string> matches;
            if (!edges.TryGetValue(edge, out matches)) {
                break;
            }

            string value = matches[config.random.Next(matches.Count)];
            chain.Add(value);
            edge = new Tuple<string, string>(edge.second, value);
        }

        if (chain.Count < config.minChainLength) {
            Debug.Log("Chain of length " + config.minChainLength + " not long enough. Trying again...");
            return ChainEdges(edges, config);
        }

        Debug.Log("Chain of length " + chain.Count + " created.");
        return chain;
    }
}
