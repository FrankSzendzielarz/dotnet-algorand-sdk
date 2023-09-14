using System.Linq;
using System;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MsgPack;


// https://www.oryx-embedded.com/doc/sha512_8c_source.html


[StructLayout(LayoutKind.Explicit)]
public struct Union
{
    [FieldOffset(0)]
    public ulong[] ulongs;

    [FieldOffset(0)]
    public byte[] bytes;
}

public class Sha512256Context : IDisposable
{
    //public ulong[] h = new ulong[8];
    //public byte[] digest = new byte[64];
    public Union H_Digest = new Union()
    {
        ulongs = new ulong[8],
        bytes = new byte[64]
    };
    //public ulong[] w = new ulong[16];
    //public byte[] buffer = new byte[128];
    public Union W_Buffer = new Union()
    {
        ulongs = new ulong[8],
        bytes = new byte[128]
    };
    public int size;
    public ulong totalSize;
    
    public void Dispose()
    {
      
    }
}

public static class Sha512256
{

    public static readonly int SHA512_BLOCK_SIZE = 128;
    public static readonly int SHA512_DIGEST_SIZE= 64;
    public static readonly int SHA512_256_DIGEST_SIZE = 32;
    public static ulong ToBigEndian(this ulong value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        return BitConverter.ToUInt64(bytes, 0);
    }

    public static ulong BEtoHost(this ulong value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        return BitConverter.ToUInt64(bytes, 0);
    }
    private static readonly ulong[] k =
        {
            0x428A2F98D728AE22UL, 0x7137449123EF65CDUL, 0xB5C0FBCFEC4D3B2FUL, 0xE9B5DBA58189DBBCUL,
            0x3956C25BF348B538UL, 0x59F111F1B605D019UL, 0x923F82A4AF194F9BUL, 0xAB1C5ED5DA6D8118UL,
            0xD807AA98A3030242UL, 0x12835B0145706FBEUL, 0x243185BE4EE4B28CUL, 0x550C7DC3D5FFB4E2UL,
            0x72BE5D74F27B896FUL, 0x80DEB1FE3B1696B1UL, 0x9BDC06A725C71235UL, 0xC19BF174CF692694UL,
            0xE49B69C19EF14AD2UL, 0xEFBE4786384F25E3UL, 0x0FC19DC68B8CD5B5UL, 0x240CA1CC77AC9C65UL,
            0x2DE92C6F592B0275UL, 0x4A7484AA6EA6E483UL, 0x5CB0A9DCBD41FBD4UL, 0x76F988DA831153B5UL,
            0x983E5152EE66DFABUL, 0xA831C66D2DB43210UL, 0xB00327C898FB213FUL, 0xBF597FC7BEEF0EE4UL,
            0xC6E00BF33DA88FC2UL, 0xD5A79147930AA725UL, 0x06CA6351E003826FUL, 0x142929670A0E6E70UL,
            0x27B70A8546D22FFCUL, 0x2E1B21385C26C926UL, 0x4D2C6DFC5AC42AEDUL, 0x53380D139D95B3DFUL,
            0x650A73548BAF63DEUL, 0x766A0ABB3C77B2A8UL, 0x81C2C92E47EDAEE6UL, 0x92722C851482353BUL,
            0xA2BFE8A14CF10364UL, 0xA81A664BBC423001UL, 0xC24B8B70D0F89791UL, 0xC76C51A30654BE30UL,
            0xD192E819D6EF5218UL, 0xD69906245565A910UL, 0xF40E35855771202AUL, 0x106AA07032BBD1B8UL,
            0x19A4C116B8D2D0C8UL, 0x1E376C085141AB53UL, 0x2748774CDF8EEB99UL, 0x34B0BCB5E19B48A8UL,
            0x391C0CB3C5C95A63UL, 0x4ED8AA4AE3418ACBUL, 0x5B9CCA4F7763E373UL, 0x682E6FF3D6B2B8A3UL,
            0x748F82EE5DEFB2FCUL, 0x78A5636F43172F60UL, 0x84C87814A1F0AB72UL, 0x8CC702081A6439ECUL,
            0x90BEFFFA23631E28UL, 0xA4506CEBDE82BDE9UL, 0xBEF9A3F7B2C67915UL, 0xC67178F2E372532BUL,
            0xCA273ECEEA26619CUL, 0xD186B8C721C0C207UL, 0xEADA7DD6CDE0EB1EUL, 0xF57D4F7FEE6ED178UL,
            0x06F067AA72176FBAUL, 0x0A637DC5A2C898A6UL, 0x113F9804BEF90DAEUL, 0x1B710B35131C471BUL,
            0x28DB77F523047D84UL, 0x32CAAB7B40C72493UL, 0x3C9EBE0A15C9BEBCUL, 0x431D67C49C100D4CUL,
            0x4CC5D4BECB3E42B6UL, 0x597F299CFC657E2AUL, 0x5FCB6FAB3AD6FAECUL, 0x6C44198C4A475817UL
         };


   
    private static ulong RotateRight(ulong x, int n)
    {
        return (((x) >> (n)) | ((x) << (64 - (n))));
    }

    //SHA-512 auxiliary functions
    private static ulong CH(ulong x, ulong y, ulong z) => ((x) & (y)) | (~(x) & (z));
    private static ulong MAJ(ulong x, ulong y, ulong z) => ((x) & (y)) | ((x) & (z)) | ((y) & (z));
    private static ulong SIGMA1(ulong x) => RotateRight(x, 28) ^ RotateRight(x, 34) ^ RotateRight(x, 39);
    private static ulong SIGMA2(ulong x) => RotateRight(x, 14) ^ RotateRight(x, 18) ^ RotateRight(x, 41);
    private static ulong SIGMA3(ulong x) => RotateRight(x, 1) ^ RotateRight(x, 8) ^ (x >> 7);
    private static ulong SIGMA4(ulong x) => RotateRight(x, 19) ^ RotateRight(x, 61) ^ (x >> 6);

    public static void Compute(byte[] data, out byte[] digest)
    {
        digest = new byte[SHA512_256_DIGEST_SIZE];
        using (Sha512256Context context = new Sha512256Context())
        {
            // Initialize the SHA-512 context
            Init(context);
            // Digest the message
            Update(context, data);
            // Finalize the SHA-512-256 message digest
            Final(context, digest);
        }
 

    }

    public static void Init(Sha512256Context context)
    {
       
        context.H_Digest.ulongs[0] = ((ulong)0x22312194FC2BF72C);
        context.H_Digest.ulongs[1] = 0x9F555FA3C84C64C2;
        context.H_Digest.ulongs[2] = ((ulong)0x2393B86B6F53B151);
        context.H_Digest.ulongs[3] = 0x963877195940EABD;
        context.H_Digest.ulongs[4] = 0x96283EE2A88EFFE3;
        context.H_Digest.ulongs[5] = 0xBE5E1E2553863992;
        context.H_Digest.ulongs[6] = ((ulong)0x2B0199FC2C85B8AA);
        context.H_Digest.ulongs[7] = ((ulong)0x0EB72DDC81C52CA2);
                                  




        context.size = 0;
        context.totalSize = 0;
    }
    public static void Update(Sha512256Context context, byte[] data)
    {
        uint length= (uint)data.Length;
        uint n;

        // Process the incoming data
        while (length > 0)
        {
            // The buffer can hold at most 128 bytes
            n = Math.Min(length, (uint)(SHA512_BLOCK_SIZE - context.size));

            // Copy the data to the buffer
            Array.Copy(data, 0, context.W_Buffer.bytes, context.size, n);

            // Update the SHA-512 context
            context.size += (int)n;
            context.totalSize += n;

            // Advance the data pointer
            data = data.Skip((int)n).ToArray();

            // Remaining bytes to process
            length -= n;

            // Process message in 16-word blocks
            if (context.size == SHA512_BLOCK_SIZE)
            {
                // Transform the 16-word block
                ProcessBlock(context);

                // Empty the buffer
                context.size = 0;
            }
        }
    }

    public static void Final(Sha512256Context context, byte[] digest)
    {
        int i;
        uint paddingSize;
        ulong totalSize;

        // Length of the original message (before padding)
        totalSize = context.totalSize * 8;

        // Pad the message so that its length is congruent to 112 modulo 128
        if (context.size < 112)
        {
            paddingSize = (uint)(112 - context.size);
        }
        else
        {
            paddingSize = (uint)(128 + 112 - context.size);
        }

        // Append padding
        var padding = new byte[] {  0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
    
        Update(context, padding.Take((int)paddingSize).ToArray());

        // Append the length of the original message
        context.W_Buffer.ulongs[14] = 0;
        context.W_Buffer.ulongs[15] = totalSize.ToBigEndian();

        // Calculate the message digest
        ProcessBlock(context);

        for (i = 0; i < 8; i++)
        {
            context.H_Digest.ulongs[i] = context.H_Digest.ulongs[i].ToBigEndian();
        }

        // Copy the resulting digest
        if (digest != null)
        {
            Array.Copy(context.H_Digest.bytes, 0, digest, 0, SHA512_256_DIGEST_SIZE);
        }
    }

    private static void ProcessBlock(Sha512256Context context)
    {
        uint t;
        ulong temp1;
        ulong temp2;

        // Initialize the 8 working registers
        ulong a = context.H_Digest.ulongs[0];
        ulong b = context.H_Digest.ulongs[1];
        ulong c = context.H_Digest.ulongs[2];
        ulong d = context.H_Digest.ulongs[3];
        ulong e = context.H_Digest.ulongs[4];
        ulong f = context.H_Digest.ulongs[5];
        ulong g = context.H_Digest.ulongs[6];
        ulong h = context.H_Digest.ulongs[7];

        // Process message in 16-word blocks
        ulong[] w = context.W_Buffer.ulongs;


       
        if (BitConverter.IsLittleEndian)
        {
            for (t = 0; t < 16; t++)
            {
                w[t] = BEtoHost(w[t]);
            }
        }

        // SHA-512 hash computation
        for (t = 0; t < 80; t++)
        {
            if (t >= 16)
            {
                w[t%16] += SIGMA4(w[(t + 14)%16]) + w[(t + 9)%16] + SIGMA3(w[(t + 1)%16]);
            }

            temp1 = (h + SIGMA2(e) + CH(e, f, g) + k[t] + w[t%16]);
            temp2 = (SIGMA1(a) + MAJ(a, b, c));

            h = g;
            g = f;
            f = e;
            e = (d + temp1);
            d = c;
            c = b;
            b = a;
            a = (temp1 + temp2);
        }

        // Update the hash value
        context.H_Digest.ulongs[0] += a;
        context.H_Digest.ulongs[1] += b;
        context.H_Digest.ulongs[2] += c;
        context.H_Digest.ulongs[3] += d;
        context.H_Digest.ulongs[4] += e;
        context.H_Digest.ulongs[5] += f;
        context.H_Digest.ulongs[6] += g;
        context.H_Digest.ulongs[7] += h;
    }
}

