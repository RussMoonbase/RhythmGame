using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Tuple<A, B> {
    public A first;
    public B second;

    public Tuple(A first, B second) {
        this.first = first;
        this.second = second;
    }

    public override bool Equals(object other) {
        if (!(other is Tuple<A, B>)) {
            return false;
        }

        Tuple<A, B> o = (Tuple<A, B>)other;
        return first.Equals(o.first) && second.Equals(o.second);
    }

    public override int GetHashCode() {
        return first.GetHashCode() + 31 * second.GetHashCode();
    }
}
