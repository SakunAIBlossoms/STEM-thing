shader_type spatial;
uniform sampler2D dither_texture: filter_nearest;
uniform float dither_scale;
uniform float dither_strength;
uniform float dither_levels;

void light() {
    float light = (dot(NORMAL, LIGHT) + 1.0) * 0.5;
    vec2 ts = vec2(textureSize(dither_texture, 0)) * dither_scale;
    vec2 uv = mod(FRAGCOORD.xy, ts) / ts;
    light += (texture(dither_texture, uv).r) * dither_strength;
    light = floor(light * dither_levels) / dither_levels;

    DIFFUSE_LIGHT += (light * LIGHT_COLOR) * ATTENUATION;

}
