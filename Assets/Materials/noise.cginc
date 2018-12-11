
	float hash(float n)
	{
		return frac(sin(n)*43758.5453);
	}

	// The noise function returns a value in the range -1.0f -> 1.0f
	float noise(float3 x)
	{
		float3 p = floor(x);
		float3 f = frac(x);

		f = f*f*(3.0 - 2.0*f);
		float n = p.x + p.y*57.0 + 113.0 * p.z;
		

		float r1 = lerp(lerp(hash(n + 0.0), hash(n + 1.0), f.x), lerp(hash(n + 57.0), hash(n + 58.0), f.x), f.y);
		float r2 = lerp(lerp(hash(n + 113.0), hash(n + 114.0), f.x), lerp(hash(n + 170.0), hash(n + 171.0), f.x), f.y);

		return lerp(r1, r2, f.z) - 0.5;
	}

	// The noise function returns a value in the range -1.0f -> 1.0f
	float noise4(float3 x, float time)
	{
		float p = floor(time);
		float f = frac(time);

		float k = 0.2;
		float r1 = noise(x + (p - 1) * k);
		float r2 = noise(x + p * k);

		return lerp(r1, r2, f);
	}

	float noiseOctaves4(float3 x, float time, int octaves)
	{
		float res = noise4(x, time);
		float freq = 1;
		float amp = 1;
		int mode = 0;
		float ampFall = 1.7 + distToCamera / 25;
		for (int i = 1; i < octaves; i++)
		{
			freq *= 2;
			amp /= ampFall;
			res += noise4(x * freq, time) * amp;
		}
		return res;
	}

	float noiseOctaves(float3 x, int octaves)
	{
		float res = noise(x);
		float freq = 1;
		float amp = 1;
		int mode = 0;
		float ampFall = 1.7 + distToCamera / 25;
		for (int i = 1; i < octaves; i++)
		{
			freq *= 2;
			amp /= ampFall;
			res += noise(x * freq) * amp;
		}
		return res;
	}
