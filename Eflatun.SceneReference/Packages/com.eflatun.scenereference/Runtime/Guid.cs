using System;

namespace Eflatun.SceneReference
{
    internal readonly struct Guid : IEquatable<Guid>
    {
        public static readonly Guid AllZero = default;

        private readonly ulong _mostSignificant;
        private readonly ulong _leastSignificant;

        public Guid(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString) || hexString.Length != 32)
            {
                throw new ArgumentException($"{nameof(hexString)} must have exactly 32 characters.", nameof(hexString));
            }

            _mostSignificant = ParseHexString(hexString, true);
            _leastSignificant = ParseHexString(hexString, false);
        }

        public override string ToString() => $"{_mostSignificant:x16}{_leastSignificant:x16}";

        public bool Equals(Guid other) => _mostSignificant == other._mostSignificant && _leastSignificant == other._leastSignificant;

        public override bool Equals(object obj) => obj is Guid other && Equals(other);

        public override int GetHashCode() => unchecked ((_mostSignificant.GetHashCode() * 397) ^ _leastSignificant.GetHashCode());

        public static bool operator ==(Guid left, Guid right) => left.Equals(right);

        public static bool operator !=(Guid left, Guid right) => !left.Equals(right);

        private static ulong ParseHexString(string hexString, bool isMostSignificant)
        {
            /* 1 ulong is 16 hexadecimal digits (64 = 16 * 4).
             * Most significant: [0, 16)
             * Least significant: [16, 32)
             */
            var startIndexInclusive = isMostSignificant ? 0 : 16;
            var endIndexExclusive = startIndexInclusive + 16;

            ulong value = 0;

            for (var i = startIndexInclusive; i < endIndexExclusive; i++)
            {
                var digitHexChar = hexString[i];
                byte digitValue;

                if ('0' <= digitHexChar && digitHexChar <= '9')
                {
                    digitValue = (byte)(digitHexChar - '0');
                }
                else if ('a' <= digitHexChar && digitHexChar <= 'f')
                {
                    digitValue = (byte)(digitHexChar - 'a' + 10);
                }
                else if ('A' <= digitHexChar && digitHexChar <= 'F')
                {
                    digitValue = (byte)(digitHexChar - 'A' + 10);
                }
                else
                {
                    throw new ArgumentException($"{nameof(hexString)} character at index {i} is '{digitHexChar}', which is not a valid hexadecimal digit.", nameof(hexString));
                }

                value = (value << 4) | digitValue;
            }

            return value;
        }
    }
}
