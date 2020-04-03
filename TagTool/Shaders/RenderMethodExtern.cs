﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagTool.Shaders
{
    public enum RenderMethodExtern : int
    {
        none,
        texture_global_target_texaccum,
        texture_global_target_normal,
        texture_global_target_z,
        texture_global_target_shadow_buffer1,
        texture_global_target_shadow_buffer2,
        texture_global_target_shadow_buffer3,
        texture_global_target_shadow_buffer4,
        texture_global_target_texture_camera,
        texture_global_target_reflection,
        texture_global_target_refraction,
        texture_lightprobe_texture,
        texture_dominant_light_intensity_map,
        texture_unused1,
        texture_unused2,
        object_change_color_primary,
        object_change_color_secondary,
        object_change_color_tertiary,
        object_change_color_quaternary,
        object_change_color_quinary,
        object_change_color_primary_anim,
        object_change_color_secondary_anim,
        texture_dynamic_environment_map_0,
        texture_dynamic_environment_map_1,
        texture_cook_torrance_cc0236,
        texture_cook_torrance_dd0236,
        texture_cook_torrance_c78d78,
        light_dir_0,
        light_color_0,
        light_dir_1,
        light_color_1,
        light_dir_2,
        light_color_2,
        light_dir_3,
        light_color_3,
        texture_unused_3,
        texture_unused_4,
        texture_unused_5,
        texture_dynamic_light_gel_0,
        flat_envmap_matrix_x,
        flat_envmap_matrix_y,
        flat_envmap_matrix_z,
        debug_tint,
        screen_constants,
        active_camo_distortion_texture,
        scene_ldr_texture,
        scene_hdr_texture,
        water_memory_export_address,
        tree_animation_timer,
        emblem_player_shoulder_texture,
        emblem_clan_chest_texture,
    }
}
