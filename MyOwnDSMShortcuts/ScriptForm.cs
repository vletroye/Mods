using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace BeatificaBytes.Synology.Mods
{
    //READ: https://github.com/jacobslusser/ScintillaNET

    public partial class ScriptForm : Form
    {
        private Scintilla scintillaScript = null;
        private Scintilla scintillaRunner = null;

        private string script = "";
        private string runner = "";

        public ScriptForm()
        {
            InitializeComponent();
        }

        public string Script
        {
            get
            {
                var value = script;
                if (value != null) value.Replace("\r\n", "\n");
                return value;
            }
            set
            {
                if (value == null)
                    tabControl.TabPages.RemoveAt(0);
                else
                {
                    if (value == string.Empty)
                        value = "#!/bin/sh\n";
                    script = value;
                }
            }
        }

        public string Runner
        {
            get
            {
                var value = runner;
                return value;
            }
            set
            {
                runner = value;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            script = scintillaScript == null ? null : scintillaScript.Text;
            runner = scintillaRunner == null ? null : scintillaRunner.Text;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            script = null;
            runner = null;
            Close();
        }

        private void ScriptForm_Load(object sender, EventArgs e)
        {
            var tab = 0;
            if (tabControl.TabPages.Count == 2)
            {
                scintillaScript = new ScintillaNET.Scintilla();
                tabControl.TabPages[0].Controls.Add(scintillaScript);
                scintillaScript.Text = script;

                InitScriptEditor(scintillaScript, Lexer.Batch);
                tab++;
            }

            scintillaRunner = new ScintillaNET.Scintilla();
            tabControl.TabPages[tab].Controls.Add(scintillaRunner);
            scintillaRunner.Text = runner;

            InitScriptEditor(scintillaRunner, Lexer.PhpScript);
        }

        private void InitScriptEditor(Scintilla textArea, Lexer type)
        {
            // BASIC CONFIG
            textArea.Dock = DockStyle.Fill;

            // INITIAL VIEW CONFIG
            textArea.WrapMode = WrapMode.None;
            textArea.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors(textArea);
            InitSyntaxColoring(textArea, type);

            // NUMBER MARGIN
            InitNumberMargin(textArea);

            // BOOKMARK MARGIN
            InitBookmarkMargin(textArea);

            // CODE FOLDING MARGIN
            InitCodeFolding(textArea);

        }

        private void InitColors(Scintilla textArea)
        {

            textArea.SetSelectionBackColor(true, Helper.IntToColor(0x114D9C));

        }

        private void InitSyntaxColoring(Scintilla textArea, Lexer type)
        {

            // Configure the default style
            textArea.StyleResetDefault();
            textArea.Styles[Style.Default].Font = "Consolas";
            textArea.Styles[Style.Default].Size = 10;
            //textArea.Styles[Style.Default].BackColor = Helper.IntToColor(0x212121);
            //textArea.Styles[Style.Default].ForeColor = Helper.IntToColor(0xFFFFFF);
            textArea.StyleClearAll();

            // Keywords from from %appdata%\notepad++\langs.xml
            // Support from https://github.com/jacobslusser/ScintillaNET/wiki
            // Style generation with https://github.com/uuf6429/ScintillaNET-Kitchen
            switch (type)
            {
                case Lexer.PhpScript:
                    textArea.Lexer = ScintillaNET.Lexer.PhpScript;

                    textArea.Styles[ScintillaNET.Style.PhpScript.ComplexVariable].ForeColor = Color.Red;
                    textArea.Styles[ScintillaNET.Style.PhpScript.HString].Bold = true;
                    textArea.Styles[ScintillaNET.Style.PhpScript.HString].ForeColor = Color.DarkViolet;
                    textArea.Styles[ScintillaNET.Style.PhpScript.HString].Weight = 700;
                    textArea.Styles[ScintillaNET.Style.PhpScript.SimpleString].ForeColor = Color.Gray;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Word].ForeColor = Color.Yellow;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Number].ForeColor = Color.DarkOrange;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Variable].ForeColor = Color.MediumBlue;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Comment].ForeColor = Color.Green;
                    textArea.Styles[ScintillaNET.Style.PhpScript.CommentLine].ForeColor = Color.Green;
                    textArea.Styles[ScintillaNET.Style.PhpScript.HStringVariable].ForeColor = Color.Navy;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Operator].ForeColor = Color.MediumOrchid;

                    textArea.SetKeywords(0, "__autoload __class__ __compiler_halt_offset__ __construct __destruct __dir__ __file__ __function__ __halt_compiler __line__ __method__ __namespace__ __sleep __trait__ __wakeup _cookie _files _get _post abday_1 abday_2 abday_3 abday_4 abday_5 abday_6 abday_7 abmon_1 abmon_10 abmon_11 abmon_12 abmon_2 abmon_3 abmon_4 abmon_5 abmon_6 abmon_7 abmon_8 abmon_9 abs abstract acos acosh addcslashes addslashes af_inet af_inet6 af_unix al_bits al_buffer al_channels al_cone_inner_angle al_cone_outer_angle al_cone_outer_gain al_direction al_false al_format_mono16 al_format_mono8 al_format_stereo16 al_format_stereo8 al_frequency al_gain al_initial al_looping al_max_distance al_max_gain al_min_gain al_orientation al_paused al_pitch al_playing al_position al_reference_distance al_rolloff_factor al_size al_source_relative al_source_state al_stopped al_true al_velocity alc_frequency alc_refresh alc_sync alt_digits am_str and apache_child_terminate apache_get_modules apache_get_version apache_getenv apache_lookup_uri apache_map apache_note apache_request_headers apache_reset_timeout apache_response_headers apache_setenv apc_add apc_bin_dump apc_bin_dumpfile apc_bin_load apc_bin_loadfile apc_bin_verify_crc32 apc_bin_verify_md5 apc_cache_info apc_cas apc_clear_cache apc_compile_file apc_dec apc_define_constants apc_delete apc_delete_file apc_exists apc_fetch apc_inc apc_iter_all apc_iter_atime apc_iter_ctime apc_iter_device apc_iter_dtime apc_iter_filename apc_iter_inode apc_iter_key apc_iter_md5 apc_iter_mem_size apc_iter_mtime apc_iter_none apc_iter_num_hits apc_iter_refcount apc_iter_ttl apc_iter_type apc_iter_value apc_list_active apc_list_deleted apc_load_constants apc_sma_info apc_store apd_breakpoint apd_callstack apd_clunk apd_continue apd_croak apd_dump_function_table apd_dump_persistent_resources apd_dump_regular_resources apd_echo apd_get_active_symbols apd_set_pprof_trace apd_set_session apd_set_session_trace apd_set_session_trace_socket apd_version args_trace array array_change_key_case array_chunk array_column array_combine array_count_values array_diff array_diff_assoc array_diff_key array_diff_uassoc array_diff_ukey array_fill array_fill_keys array_filter array_filter_use_both array_filter_use_key array_flip array_intersect array_intersect_assoc array_intersect_key array_intersect_uassoc array_intersect_ukey array_key_exists array_keys array_map array_merge array_merge_recursive array_multisort array_pad array_pop array_product array_push array_rand array_reduce array_replace array_replace_recursive array_reverse array_search array_shift array_slice array_sort array_splice array_sum array_udiff array_udiff_assoc array_udiff_uassoc array_uintersect array_uintersect_assoc array_uintersect_uassoc array_unique array_unshift array_values array_walk array_walk_recursive arrayaccess arrayiterator arrayobject arsort as asin asinh asort assert assert_active assert_bail assert_callback assert_options assert_quiet_eval assert_warning assignment_trace atan atan2 atanh base64_decode base64_encode base_convert basename bbcode_add_element bbcode_add_smiley bbcode_arg_double_quote bbcode_arg_html_quote bbcode_arg_quote_escaping bbcode_arg_single_quote bbcode_auto_correct bbcode_correct_reopen_tags bbcode_create bbcode_default_smileys_off bbcode_default_smileys_on bbcode_destroy bbcode_disable_tree_build bbcode_flags_arg_parsing bbcode_flags_cdata_not_allowed bbcode_flags_deny_reopen_child bbcode_flags_one_open_per_level bbcode_flags_remove_if_empty bbcode_flags_smileys_off bbcode_flags_smileys_on bbcode_force_smileys_off bbcode_parse bbcode_set_arg_parser bbcode_set_flags bbcode_set_flags_add bbcode_set_flags_remove bbcode_set_flags_set bbcode_smileys_case_insensitive bbcode_type_arg bbcode_type_noarg bbcode_type_optarg bbcode_type_root bbcode_type_single bcadd bccomp bcdiv bcmod bcmul bcompiler_load bcompiler_load_exe bcompiler_parse_class bcompiler_read bcompiler_write_class bcompiler_write_constant bcompiler_write_exe_footer bcompiler_write_file bcompiler_write_footer bcompiler_write_function bcompiler_write_functions_from_file bcompiler_write_header bcompiler_write_included_filename bcpow bcpowmod bcscale bcsqrt bcsub bin2hex bind_textdomain_codeset bindec bindtextdomain blenc_encrypt blenc_ext_version bool boolean boolval break bson_decode bson_encode bus_adraln bus_adrerr bus_objerr bzclose bzcompress bzdecompress bzerrno bzerror bzerrstr bzflush bzopen bzread bzwrite cairo_antialias_default cairo_antialias_gray cairo_antialias_none cairo_antialias_subpixel cairo_content_alpha cairo_content_color cairo_content_color_alpha cairo_create cairo_extend_none cairo_extend_pad cairo_extend_reflect cairo_extend_repeat cairo_fill_rule_even_odd cairo_fill_rule_winding cairo_filter_best cairo_filter_bilinear cairo_filter_fast cairo_filter_gaussian cairo_filter_good cairo_filter_nearest cairo_font_face_get_type cairo_font_face_status cairo_font_options_create cairo_font_options_equal cairo_font_options_get_antialias cairo_font_options_get_hint_metrics cairo_font_options_get_hint_style cairo_font_options_get_subpixel_order cairo_font_options_hash cairo_font_options_merge cairo_font_options_set_antialias cairo_font_options_set_hint_metrics cairo_font_options_set_hint_style cairo_font_options_set_subpixel_order cairo_font_options_status cairo_font_slant_italic cairo_font_slant_normal cairo_font_slant_oblique cairo_font_type_ft cairo_font_type_quartz cairo_font_type_toy cairo_font_type_win32 cairo_font_weight_bold cairo_font_weight_normal cairo_format_a1 cairo_format_a8 cairo_format_argb32 cairo_format_rgb24 cairo_format_stride_for_width cairo_hint_metrics_default cairo_hint_metrics_off cairo_hint_metrics_on cairo_hint_style_default cairo_hint_style_full cairo_hint_style_medium cairo_hint_style_none cairo_hint_style_slight cairo_image_surface_create cairo_image_surface_create_for_data cairo_image_surface_create_from_png cairo_image_surface_get_data cairo_image_surface_get_format cairo_image_surface_get_height cairo_image_surface_get_stride cairo_image_surface_get_width cairo_line_cap_butt cairo_line_cap_round cairo_line_cap_square cairo_line_join_bevel cairo_line_join_miter cairo_line_join_round cairo_matrix_create_scale cairo_matrix_create_translate cairo_matrix_invert cairo_matrix_multiply cairo_matrix_rotate cairo_matrix_transform_distance cairo_matrix_transform_point cairo_matrix_translate cairo_operator_add cairo_operator_atop cairo_operator_clear cairo_operator_dest cairo_operator_dest_atop cairo_operator_dest_in cairo_operator_dest_out cairo_operator_dest_over cairo_operator_in cairo_operator_out cairo_operator_over cairo_operator_saturate cairo_operator_source cairo_operator_xor cairo_pattern_add_color_stop_rgb cairo_pattern_add_color_stop_rgba cairo_pattern_create_for_surface cairo_pattern_create_linear cairo_pattern_create_radial cairo_pattern_create_rgb cairo_pattern_create_rgba cairo_pattern_get_color_stop_count cairo_pattern_get_color_stop_rgba cairo_pattern_get_extend cairo_pattern_get_filter cairo_pattern_get_linear_points cairo_pattern_get_matrix cairo_pattern_get_radial_circles cairo_pattern_get_rgba cairo_pattern_get_surface cairo_pattern_get_type cairo_pattern_set_extend cairo_pattern_set_filter cairo_pattern_set_matrix cairo_pattern_status cairo_pattern_type_linear cairo_pattern_type_radial cairo_pattern_type_solid cairo_pattern_type_surface cairo_pdf_surface_create cairo_pdf_surface_set_size cairo_ps_get_levels cairo_ps_level_2 cairo_ps_level_3 cairo_ps_level_to_string cairo_ps_surface_create cairo_ps_surface_dsc_begin_page_setup cairo_ps_surface_dsc_begin_setup cairo_ps_surface_dsc_comment cairo_ps_surface_get_eps cairo_ps_surface_restrict_to_level cairo_ps_surface_set_eps cairo_ps_surface_set_size cairo_scaled_font_create cairo_scaled_font_extents cairo_scaled_font_get_ctm cairo_scaled_font_get_font_face cairo_scaled_font_get_font_matrix cairo_scaled_font_get_font_options cairo_scaled_font_get_scale_matrix cairo_scaled_font_get_type cairo_scaled_font_glyph_extents cairo_scaled_font_status cairo_scaled_font_text_extents cairo_status_clip_not_representable cairo_status_file_not_found cairo_status_invalid_content cairo_status_invalid_dash cairo_status_invalid_dsc_comment cairo_status_invalid_format cairo_status_invalid_index cairo_status_invalid_matrix cairo_status_invalid_path_data cairo_status_invalid_pop_group cairo_status_invalid_restore cairo_status_invalid_status cairo_status_invalid_stride cairo_status_invalid_string cairo_status_invalid_visual cairo_status_no_current_point cairo_status_no_memory cairo_status_null_pointer cairo_status_pattern_type_mismatch cairo_status_read_error cairo_status_success cairo_status_surface_finished cairo_status_surface_type_mismatch cairo_status_temp_file_error cairo_status_write_error cairo_subpixel_order_bgr cairo_subpixel_order_default cairo_subpixel_order_rgb cairo_subpixel_order_vbgr cairo_subpixel_order_vrgb cairo_surface_copy_page cairo_surface_create_similar cairo_surface_finish cairo_surface_flush cairo_surface_get_content cairo_surface_get_device_offset cairo_surface_get_font_options cairo_surface_get_type cairo_surface_mark_dirty cairo_surface_mark_dirty_rectangle cairo_surface_set_device_offset cairo_surface_set_fallback_resolution cairo_surface_show_page cairo_surface_status cairo_surface_type_beos cairo_surface_type_directfb cairo_surface_type_glitz cairo_surface_type_image cairo_surface_type_os2 cairo_surface_type_pdf cairo_surface_type_ps cairo_surface_type_quartz cairo_surface_type_quartz_image cairo_surface_type_svg cairo_surface_type_win32 cairo_surface_type_win32_printing cairo_surface_type_xcb cairo_surface_type_xlib cairo_surface_write_to_png cairo_svg_surface_create cairo_svg_surface_restrict_to_version cairo_svg_version_1_1 cairo_svg_version_1_2 cairo_svg_version_to_string cal_days_in_month cal_dow_dayno cal_dow_long cal_dow_short cal_easter_always_gregorian cal_easter_always_julian cal_easter_default cal_easter_roman cal_french cal_from_jd cal_gregorian cal_info cal_jewish cal_jewish_add_alafim cal_jewish_add_alafim_geresh cal_jewish_add_gereshayim cal_julian cal_month_french cal_month_gregorian_long cal_month_gregorian_short cal_month_jewish cal_month_julian_long cal_month_julian_short cal_num_cals cal_to_jd calcul_hmac calculhmac call_user_func call_user_func_array call_user_method call_user_method_array callable case case_lower case_upper catch ceil cfunction char_max chdb_create chdir checkdate checkdnsrr chgrp chmod chop chown chr chroot chunk_split cl_expunge class class_alias class_exists class_implements class_parents class_uses cld_continued cld_dumped cld_exited cld_killed cld_stopped cld_trapped clearstatcache cli_get_process_title cli_set_process_title clone closedir closelog clsctx_all clsctx_inproc_handler clsctx_inproc_server clsctx_local_server clsctx_remote_server clsctx_server codeset collator_asort collator_compare collator_create collator_get_attribute collator_get_error_code collator_get_error_message collator_get_locale collator_get_sort_key collator_get_strength collator_set_attribute collator_set_strength collator_sort collator_sort_with_sort_keys com_create_guid com_event_sink com_get com_get_active_object com_invoke com_load com_load_typelib com_message_pump com_print_typeinfo com_propget com_propput com_set compact connection_aborted connection_normal connection_status connection_timeout const constant continue convert_cyr_string convert_uudecode convert_uuencode copy cos cosh count count_chars count_normal count_recursive cp_acp cp_maccp cp_move cp_oemcp cp_symbol cp_thread_acp cp_uid cp_utf7 cp_utf8 crc32 create_function credits_all credits_docs credits_fullpage credits_general credits_group credits_modules credits_qa credits_sapi crncystr crypt crypt_blowfish crypt_ext_des crypt_md5 crypt_salt_length crypt_sha256 crypt_sha512 crypt_std_des ctype_alnum ctype_alpha ctype_cntrl ctype_digit ctype_graph ctype_lower ctype_print ctype_punct ctype_space ctype_upper ctype_xdigit cubrid_affected_rows cubrid_assoc cubrid_async cubrid_autocommit_false cubrid_autocommit_true cubrid_bind cubrid_both cubrid_client_encoding cubrid_close cubrid_close_prepare cubrid_close_request cubrid_col_get cubrid_col_size cubrid_column_names cubrid_column_types cubrid_commit cubrid_connect cubrid_connect_with_url cubrid_current_oid cubrid_cursor_current cubrid_cursor_error cubrid_cursor_first cubrid_cursor_last cubrid_cursor_success cubrid_data_seek cubrid_db_name cubrid_disconnect cubrid_drop cubrid_errno cubrid_error cubrid_error_code cubrid_error_code_facility cubrid_error_msg cubrid_exec_query_all cubrid_execute cubrid_facility_cas cubrid_facility_cci cubrid_facility_client cubrid_facility_dbms cubrid_fetch cubrid_fetch_array cubrid_fetch_assoc cubrid_fetch_field cubrid_fetch_lengths cubrid_fetch_object cubrid_fetch_row cubrid_field_flags cubrid_field_len cubrid_field_name cubrid_field_seek cubrid_field_table cubrid_field_type cubrid_free_result cubrid_get cubrid_get_autocommit cubrid_get_charset cubrid_get_class_name cubrid_get_client_info cubrid_get_db_parameter cubrid_get_query_timeout cubrid_get_server_info cubrid_include_oid cubrid_insert_id cubrid_is_instance cubrid_list_dbs cubrid_lob cubrid_lob2_bind cubrid_lob2_close cubrid_lob2_export cubrid_lob2_import cubrid_lob2_new cubrid_lob2_read cubrid_lob2_seek cubrid_lob2_seek64 cubrid_lob2_size cubrid_lob2_size64 cubrid_lob2_tell cubrid_lob2_tell64 cubrid_lob2_write cubrid_lob_close cubrid_lob_export cubrid_lob_get cubrid_lob_send cubrid_lob_size cubrid_lock_read cubrid_lock_write cubrid_move_cursor cubrid_next_result cubrid_no_more_data cubrid_num cubrid_num_cols cubrid_num_fields cubrid_num_rows cubrid_object cubrid_param_isolation_level cubrid_param_lock_timeout cubrid_pconnect cubrid_pconnect_with_url cubrid_ping cubrid_prepare cubrid_put cubrid_query cubrid_real_escape_string cubrid_result cubrid_rollback cubrid_sch_attr_privilege cubrid_sch_attribute cubrid_sch_class cubrid_sch_class_attribute cubrid_sch_class_method cubrid_sch_class_privilege cubrid_sch_constraint cubrid_sch_cross_reference cubrid_sch_direct_super_class cubrid_sch_exported_keys cubrid_sch_imported_keys cubrid_sch_method cubrid_sch_method_file cubrid_sch_primary_key cubrid_sch_query_spec cubrid_sch_subclass cubrid_sch_superclass cubrid_sch_trigger cubrid_sch_vclass cubrid_schema cubrid_seq_drop cubrid_seq_insert cubrid_seq_put cubrid_set_add cubrid_set_autocommit cubrid_set_db_parameter cubrid_set_drop cubrid_set_query_timeout cubrid_unbuffered_query cubrid_version curl_close curl_copy_handle curl_errno curl_error curl_escape curl_exec curl_file_create curl_fnmatchfunc_fail curl_fnmatchfunc_match curl_fnmatchfunc_nomatch curl_getinfo curl_http_version_1_0 curl_http_version_1_1 curl_http_version_2_0 curl_http_version_none curl_init curl_ipresolve_v4 curl_ipresolve_v6 curl_ipresolve_whatever curl_lock_data_cookie curl_lock_data_dns curl_lock_data_ssl_session curl_multi_add_handle curl_multi_close curl_multi_exec curl_multi_getcontent curl_multi_info_read curl_multi_init curl_multi_remove_handle curl_multi_select curl_multi_setopt curl_multi_strerror curl_netrc_ignored curl_netrc_optional curl_netrc_required curl_pause curl_readfunc_pause curl_reset curl_rtspreq_announce curl_rtspreq_describe curl_rtspreq_get_parameter curl_rtspreq_options curl_rtspreq_pause curl_rtspreq_play curl_rtspreq_receive curl_rtspreq_record curl_rtspreq_set_parameter curl_rtspreq_setup curl_rtspreq_teardown curl_setopt curl_setopt_array curl_share_close curl_share_init curl_share_setopt curl_sslversion_default curl_sslversion_sslv2 curl_sslversion_sslv3 curl_sslversion_tlsv1 curl_sslversion_tlsv1_0 curl_sslversion_tlsv1_1 curl_sslversion_tlsv1_2 curl_strerror curl_timecond_ifmodsince curl_timecond_ifunmodsince curl_timecond_lastmod curl_timecond_none curl_tlsauth_srp curl_unescape curl_version curl_version_http2 curl_version_ipv6 curl_version_kerberos4 curl_version_libz curl_version_ssl curl_wrappers_enabled curl_writefunc_pause curlauth_any curlauth_anysafe curlauth_basic curlauth_digest curlauth_digest_ie curlauth_gssnegotiate curlauth_none curlauth_ntlm curlauth_only curlclosepolicy_callback curlclosepolicy_least_recently_used curlclosepolicy_least_traffic curlclosepolicy_oldest curlclosepolicy_slowest curle_aborted_by_callback curle_bad_calling_order curle_bad_content_encoding curle_bad_download_resume curle_bad_function_argument curle_bad_password_entered curle_couldnt_connect curle_couldnt_resolve_host curle_couldnt_resolve_proxy curle_failed_init curle_file_couldnt_read_file curle_filesize_exceeded curle_ftp_access_denied curle_ftp_bad_download_resume curle_ftp_cant_get_host curle_ftp_cant_reconnect curle_ftp_couldnt_get_size curle_ftp_couldnt_retr_file curle_ftp_couldnt_set_ascii curle_ftp_couldnt_set_binary curle_ftp_couldnt_stor_file curle_ftp_couldnt_use_rest curle_ftp_partial_file curle_ftp_port_failed curle_ftp_quote_error curle_ftp_ssl_failed curle_ftp_user_password_incorrect curle_ftp_weird_227_format curle_ftp_weird_pass_reply curle_ftp_weird_pasv_reply curle_ftp_weird_server_reply curle_ftp_weird_user_reply curle_ftp_write_error curle_function_not_found curle_got_nothing curle_http_not_found curle_http_port_failed curle_http_post_error curle_http_range_error curle_http_returned_error curle_ldap_cannot_bind curle_ldap_invalid_url curle_ldap_search_failed curle_library_not_found curle_malformat_user curle_obsolete curle_ok curle_operation_timedout curle_operation_timeouted curle_out_of_memory curle_partial_file curle_read_error curle_recv_error curle_send_error curle_share_in_use curle_ssh curle_ssl_cacert curle_ssl_certproblem curle_ssl_cipher curle_ssl_connect_error curle_ssl_engine_notfound curle_ssl_engine_setfailed curle_ssl_peer_certificate curle_telnet_option_syntax curle_too_many_redirects curle_unknown_telnet_option curle_unsupported_protocol curle_url_malformat curle_url_malformat_user curle_write_error curlftpauth_default curlftpauth_ssl curlftpauth_tls curlftpmethod_multicwd curlftpmethod_nocwd curlftpmethod_singlecwd curlftpssl_all curlftpssl_ccc_active curlftpssl_ccc_none curlftpssl_ccc_passive curlftpssl_control curlftpssl_none curlftpssl_try curlgssapi_delegation_flag curlgssapi_delegation_policy_flag curlinfo_appconnect_time curlinfo_certinfo curlinfo_condition_unmet curlinfo_connect_time curlinfo_content_length_download curlinfo_content_length_upload curlinfo_content_type curlinfo_cookielist curlinfo_effective_url curlinfo_filetime curlinfo_ftp_entry_path curlinfo_header_out curlinfo_header_size curlinfo_http_code curlinfo_http_connectcode curlinfo_httpauth_avail curlinfo_lastone curlinfo_local_ip curlinfo_local_port curlinfo_namelookup_time curlinfo_num_connects curlinfo_os_errno curlinfo_pretransfer_time curlinfo_primary_ip curlinfo_primary_port curlinfo_private curlinfo_proxyauth_avail curlinfo_redirect_count curlinfo_redirect_time curlinfo_redirect_url curlinfo_request_size curlinfo_response_code curlinfo_rtsp_client_cseq curlinfo_rtsp_cseq_recv curlinfo_rtsp_server_cseq curlinfo_rtsp_session_id curlinfo_size_download curlinfo_size_upload curlinfo_speed_download curlinfo_speed_upload curlinfo_ssl_engines curlinfo_ssl_verifyresult curlinfo_starttransfer_time curlinfo_total_time curlm_bad_easy_handle curlm_bad_handle curlm_call_multi_perform curlm_internal_error curlm_ok curlm_out_of_memory curlmopt_maxconnects curlmopt_pipelining curlmsg_done curlopt_accept_encoding curlopt_accepttimeout_ms curlopt_address_scope curlopt_append curlopt_autoreferer curlopt_binarytransfer curlopt_buffersize curlopt_cainfo                 curlopt_capath curlopt_certinfo curlopt_closepolicy curlopt_connect_only curlopt_connecttimeout curlopt_connecttimeout_ms curlopt_cookie curlopt_cookiefile curlopt_cookiejar curlopt_cookielist curlopt_cookiesession curlopt_crlf curlopt_crlfile curlopt_customrequest curlopt_dirlistonly curlopt_dns_cache_timeout curlopt_dns_servers curlopt_dns_use_global_cache curlopt_egdsocket curlopt_encoding curlopt_failonerror curlopt_file curlopt_filetime curlopt_fnmatch_function curlopt_followlocation curlopt_forbid_reuse curlopt_fresh_connect curlopt_ftp_account curlopt_ftp_alternative_to_user curlopt_ftp_create_missing_dirs curlopt_ftp_filemethod curlopt_ftp_response_timeout curlopt_ftp_skip_pasv_ip curlopt_ftp_ssl curlopt_ftp_ssl_ccc curlopt_ftp_use_eprt curlopt_ftp_use_epsv curlopt_ftp_use_pret curlopt_ftpappend curlopt_ftpascii curlopt_ftplistonly curlopt_ftpport curlopt_ftpsslauth curlopt_gssapi_delegation curlopt_header curlopt_headerfunction curlopt_http200aliases curlopt_http_content_decoding curlopt_http_transfer_decoding curlopt_http_version curlopt_httpauth curlopt_httpget curlopt_httpheader curlopt_httpproxytunnel curlopt_ignore_content_length curlopt_infile curlopt_infilesize curlopt_interface curlopt_ipresolve curlopt_issuercert curlopt_keypasswd curlopt_krb4level curlopt_krblevel curlopt_localport curlopt_localportrange curlopt_low_speed_limit curlopt_low_speed_time curlopt_mail_auth curlopt_mail_from curlopt_mail_rcpt curlopt_max_recv_speed_large curlopt_max_send_speed_large curlopt_maxconnects curlopt_maxfilesize curlopt_maxredirs curlopt_mute curlopt_netrc curlopt_netrc_file curlopt_new_directory_perms curlopt_new_file_perms curlopt_nobody curlopt_noprogress curlopt_noproxy curlopt_nosignal curlopt_passwdfunction curlopt_password curlopt_port curlopt_post curlopt_postfields curlopt_postquote curlopt_postredir curlopt_prequote curlopt_private curlopt_progressfunction curlopt_protocols curlopt_proxy curlopt_proxy_transfer_mode curlopt_proxyauth curlopt_proxypassword curlopt_proxyport curlopt_proxytype curlopt_proxyusername curlopt_proxyuserpwd curlopt_put curlopt_quote curlopt_random_file curlopt_range curlopt_readdata curlopt_readfunction curlopt_redir_protocols curlopt_referer curlopt_resolve curlopt_resume_from curlopt_returntransfer curlopt_rtsp_client_cseq curlopt_rtsp_request curlopt_rtsp_server_cseq curlopt_rtsp_session_id curlopt_rtsp_stream_uri curlopt_rtsp_transport curlopt_safe_upload curlopt_share curlopt_socks5_gssapi_nec curlopt_socks5_gssapi_service curlopt_ssh_auth_types curlopt_ssh_host_public_key_md5 curlopt_ssh_knownhosts curlopt_ssh_private_keyfile curlopt_ssh_public_keyfile curlopt_ssl_cipher_list curlopt_ssl_options curlopt_ssl_sessionid_cache curlopt_ssl_verifyhost curlopt_ssl_verifypeer curlopt_sslcert curlopt_sslcertpasswd curlopt_sslcerttype curlopt_sslengine curlopt_sslengine_default curlopt_sslkey curlopt_sslkeypasswd curlopt_sslkeytype curlopt_sslversion curlopt_stderr curlopt_tcp_keepalive curlopt_tcp_keepidle curlopt_tcp_keepintvl curlopt_tcp_nodelay curlopt_telnetoptions curlopt_tftp_blksize curlopt_timecondition curlopt_timeout curlopt_timeout_ms curlopt_timevalue curlopt_tlsauth_password curlopt_tlsauth_type curlopt_tlsauth_username curlopt_transfer_encoding curlopt_transfertext curlopt_unrestricted_auth curlopt_upload curlopt_url curlopt_use_ssl curlopt_useragent curlopt_username curlopt_userpwd curlopt_verbose curlopt_wildcardmatch curlopt_writefunction curlopt_writeheader curlpause_all curlpause_cont curlpause_recv curlpause_recv_cont curlpause_send curlpause_send_cont curlproto_all curlproto_dict curlproto_file curlproto_ftp curlproto_ftps curlproto_gopher curlproto_http curlproto_https curlproto_imap curlproto_imaps curlproto_ldap curlproto_ldaps curlproto_pop3 curlproto_pop3s curlproto_rtmp curlproto_rtmpe curlproto_rtmps curlproto_rtmpt curlproto_rtmpte curlproto_rtmpts curlproto_rtsp curlproto_scp curlproto_sftp curlproto_smtp curlproto_smtps curlproto_telnet curlproto_tftp curlproxy_http curlproxy_socks4 curlproxy_socks4a curlproxy_socks5 curlproxy_socks5_hostname curlshopt_none curlshopt_share curlshopt_unshare curlssh_auth_any curlssh_auth_default curlssh_auth_host curlssh_auth_keyboard curlssh_auth_none curlssh_auth_password curlssh_auth_publickey curlsslopt_allow_beast curlusessl_all curlusessl_control curlusessl_none curlusessl_try curlversion_now currency_symbol current cyrus_authenticate cyrus_bind cyrus_callback_noliteral cyrus_callback_numbered cyrus_close cyrus_conn_initialresponse cyrus_conn_nonsyncliteral cyrus_connect cyrus_query cyrus_unbind d_fmt d_t_fmt date date_add date_atom date_cookie date_create date_create_from_format date_create_immutable date_create_immutable_from_format date_date_set date_default_timezone_get date_default_timezone_set date_diff date_format date_get_last_errors date_interval_create_from_date_string date_interval_format date_iso8601 date_isodate_set date_modify date_offset_get date_parse date_parse_from_format date_rfc1036 date_rfc1123 date_rfc2822 date_rfc3339 date_rfc822 date_rfc850 date_rss date_sub date_sun_info date_sunrise date_sunset date_time_set date_timestamp_get date_timestamp_set date_timezone_get date_timezone_set date_w3c datefmt_create datefmt_format datefmt_get_calendar datefmt_get_datetype datefmt_get_error_code datefmt_get_error_message datefmt_get_locale datefmt_get_pattern datefmt_get_timetype datefmt_get_timezone_id datefmt_is_lenient datefmt_localtime datefmt_parse datefmt_set_calendar datefmt_set_lenient datefmt_set_pattern datefmt_set_timezone_id day_1 day_2 day_3 day_4 day_5 day_6 day_7 db2_autocommit db2_autocommit_off db2_autocommit_on db2_binary db2_bind_param db2_case_lower db2_case_natural db2_case_upper db2_char db2_client_info db2_close db2_column_privileges db2_columns db2_commit db2_conn_error db2_conn_errormsg db2_connect db2_convert db2_cursor_type db2_deferred_prepare_off db2_deferred_prepare_on db2_double db2_escape_string db2_exec db2_execute db2_fetch_array db2_fetch_assoc db2_fetch_both db2_fetch_object db2_fetch_row db2_field_display_size db2_field_name db2_field_num db2_field_precision db2_field_scale db2_field_type db2_field_width db2_foreign_keys db2_forward_only db2_free_result db2_free_stmt db2_get_option db2_last_insert_id db2_lob_read db2_long db2_next_result db2_num_fields db2_num_rows db2_param_file db2_param_in db2_param_inout db2_param_out db2_passthru db2_pclose db2_pconnect db2_prepare db2_primary_keys db2_procedure_columns db2_procedures db2_result db2_rollback db2_scrollable db2_server_info db2_set_option db2_special_columns db2_statistics db2_stmt_error db2_stmt_errormsg db2_table_privileges db2_tables dba_close dba_delete dba_exists dba_fetch dba_firstkey dba_handlers dba_insert dba_key_split dba_list dba_nextkey dba_open dba_optimize dba_popen dba_replace dba_sync dbase_add_record dbase_close dbase_create dbase_delete_record dbase_get_header_info dbase_get_record dbase_get_record_with_names dbase_numfields dbase_numrecords dbase_open dbase_pack dbase_replace_record dbplus_add dbplus_aql dbplus_chdir dbplus_close dbplus_curr dbplus_err_close dbplus_err_corrupt_tuple dbplus_err_crc dbplus_err_create dbplus_err_dbparse dbplus_err_dbpreexit dbplus_err_dbrunerr dbplus_err_duplicate dbplus_err_empty dbplus_err_eoscan dbplus_err_fifo dbplus_err_length dbplus_err_locked dbplus_err_lseek dbplus_err_magic dbplus_err_malloc dbplus_err_nidx dbplus_err_noerr dbplus_err_nolock dbplus_err_nusers dbplus_err_ontrap dbplus_err_open dbplus_err_panic dbplus_err_perm dbplus_err_pgsize dbplus_err_pipe dbplus_err_preexit dbplus_err_preproc dbplus_err_read dbplus_err_restricted dbplus_err_tcl dbplus_err_unknown dbplus_err_user dbplus_err_version dbplus_err_wait dbplus_err_warning0 dbplus_err_wlocked dbplus_err_wopen dbplus_err_write dbplus_errcode dbplus_errno dbplus_find dbplus_first dbplus_flush dbplus_freealllocks dbplus_freelock dbplus_freerlocks dbplus_getlock dbplus_getunique dbplus_info dbplus_last dbplus_lockrel dbplus_next dbplus_open dbplus_prev dbplus_rchperm dbplus_rcreate dbplus_rcrtexact dbplus_rcrtlike dbplus_resolve dbplus_restorepos dbplus_rkeys dbplus_ropen dbplus_rquery dbplus_rrename dbplus_rsecindex dbplus_runlink dbplus_rzap dbplus_savepos dbplus_setindex dbplus_setindexbynumber dbplus_sql dbplus_tcl dbplus_tremove dbplus_undo dbplus_undoprepare dbplus_unlockrel dbplus_unselect dbplus_update dbplus_xlockrel dbplus_xunlockrel dbx_close dbx_cmp_asc dbx_cmp_desc dbx_cmp_native dbx_cmp_number dbx_cmp_text dbx_colnames_lowercase dbx_colnames_unchanged dbx_colnames_uppercase dbx_compare dbx_connect dbx_error dbx_escape_string dbx_fbsql dbx_fetch_row dbx_mssql dbx_mysql dbx_oci8 dbx_odbc dbx_persistent dbx_pgsql dbx_query dbx_result_assoc dbx_result_index dbx_result_info dbx_result_unbuffered dbx_sort dbx_sqlite dbx_sybasect dcgettext dcngettext debug_backtrace debug_backtrace_ignore_args debug_backtrace_provide_object debug_print_backtrace debug_zval_dump decbin dechex decimal_point declare decoct default default_include_path define define_syslog_variables defined deg2rad delete dgettext die dio_close dio_fcntl dio_open dio_read dio_seek dio_stat dio_tcsetattr dio_truncate dio_write dir directory directory_separator dirname disk_free_space disk_total_space diskfreespace disp_e_badindex disp_e_divbyzero disp_e_overflow dl dngettext dns_a dns_a6 dns_aaaa dns_all dns_any dns_check_record dns_cname dns_get_mx dns_get_record dns_hinfo dns_mx dns_naptr dns_ns dns_ptr dns_soa dns_srv dns_txt do dom_hierarchy_request_err dom_import_simplexml dom_index_size_err dom_inuse_attribute_err dom_invalid_access_err dom_invalid_character_err dom_invalid_modification_err dom_invalid_state_err dom_namespace_err dom_no_data_allowed_err dom_no_modification_allowed_err dom_not_found_err                 dom_not_supported_err dom_php_err dom_syntax_err dom_validation_err dom_wrong_document_err domstring_size_err double doubleval e_all e_compile_error e_compile_warning e_core_error e_core_warning e_deprecated e_error e_notice e_parse e_recoverable_error e_strict e_user_deprecated e_user_error e_user_notice e_user_warning e_warning each easter_date easter_days echo eio_busy eio_cancel eio_chmod eio_chown eio_close eio_custom eio_dt_blk eio_dt_chr eio_dt_cmp eio_dt_dir eio_dt_door eio_dt_fifo eio_dt_lnk eio_dt_max eio_dt_mpb eio_dt_mpc eio_dt_nam eio_dt_nwk eio_dt_reg eio_dt_sock eio_dt_unknown eio_dt_wht eio_dup2 eio_event_loop eio_falloc_fl_keep_size eio_fallocate eio_fchmod eio_fchown eio_fdatasync eio_fstat eio_fstatvfs eio_fsync eio_ftruncate eio_futime eio_get_event_stream eio_get_last_error eio_grp eio_grp_add eio_grp_cancel eio_grp_limit eio_init eio_link eio_lstat eio_mkdir eio_mknod eio_nop eio_npending eio_nready eio_nreqs eio_nthreads eio_o_append eio_o_creat eio_o_excl eio_o_fsync eio_o_nonblock eio_o_rdonly eio_o_rdwr eio_o_trunc eio_o_wronly eio_open eio_poll eio_pri_default eio_pri_max eio_pri_min eio_read eio_readahead eio_readdir eio_readdir_dents eio_readdir_dirs_first eio_readdir_found_unknown eio_readdir_stat_order eio_readlink eio_realpath eio_rename eio_rmdir eio_s_ifblk eio_s_ifchr eio_s_ififo eio_s_ifreg eio_s_ifsock eio_s_irgrp eio_s_iroth eio_s_irusr eio_s_iwgrp eio_s_iwoth eio_s_iwusr eio_s_ixgrp eio_s_ixoth eio_s_ixusr eio_seek eio_seek_cur eio_seek_end eio_seek_set eio_sendfile eio_set_max_idle eio_set_max_parallel eio_set_max_poll_reqs eio_set_max_poll_time eio_set_min_parallel eio_stat eio_statvfs eio_symlink eio_sync eio_sync_file_range eio_sync_file_range_wait_after eio_sync_file_range_wait_before eio_sync_file_range_write eio_syncfs eio_truncate eio_unlink eio_utime eio_write else elseif empty enc7bit enc8bit encbase64 encbinary encother encquotedprintable end enddeclare endfor endforeach endif endswitch endwhile ent_compat ent_disallowed ent_html401 ent_html5 ent_ignore ent_noquotes ent_quotes ent_substitute ent_xhtml ent_xml1 era era_d_fmt era_d_t_fmt era_t_fmt era_year ereg ereg_replace eregi eregi_replace error_get_last error_log error_reporting error_trace escapeshellarg escapeshellcmd ev_persist ev_read ev_signal ev_timeout ev_write eval event_add event_base_free event_base_loop event_base_loopbreak event_base_loopexit event_base_new event_base_priority_init event_base_reinit event_base_set event_buffer_base_set event_buffer_disable event_buffer_enable event_buffer_fd_set event_buffer_free event_buffer_new event_buffer_priority_set event_buffer_read event_buffer_set_callback event_buffer_timeout_set event_buffer_watermark_set event_buffer_write event_del event_free event_new event_priority_set event_set event_timer_add event_timer_del event_timer_new event_timer_set evloop_nonblock evloop_once exec exif_imagetype exif_read_data exif_tagname exif_thumbnail exif_use_mbstring exit exp exp_eof exp_exact exp_fullbuffer exp_glob exp_regexp exp_timeout expect_expectl expect_popen explode expm1 extends extension_loaded extr_if_exists extr_overwrite extr_prefix_all extr_prefix_if_exists extr_prefix_invalid extr_prefix_same extr_refs extr_skip extract ezmlm_hash f_dupfd f_getfd f_getfl f_getlk f_getown f_rdlck f_setfl f_setlk f_setlkw f_setown f_unlck f_wrlck false fann_c");
                    break;
                case Lexer.Batch:
                    textArea.Lexer = ScintillaNET.Lexer.PhpScript;

                    textArea.Styles[ScintillaNET.Style.PhpScript.ComplexVariable].ForeColor = Color.Red;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Default].Bold = true;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Default].ForeColor = Color.RoyalBlue;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Default].Weight = 700;
                    textArea.Styles[ScintillaNET.Style.PhpScript.HString].ForeColor = Color.Gray;
                    textArea.Styles[ScintillaNET.Style.PhpScript.SimpleString].ForeColor = Color.Blue;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Word].ForeColor = Color.Yellow;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Number].ForeColor = Color.Fuchsia;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Variable].ForeColor = Color.DarkOrange;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Comment].ForeColor = Color.Maroon;
                    textArea.Styles[ScintillaNET.Style.PhpScript.CommentLine].ForeColor = Color.Green;
                    textArea.Styles[ScintillaNET.Style.PhpScript.HStringVariable].BackColor = Color.Yellow;
                    textArea.Styles[ScintillaNET.Style.PhpScript.HStringVariable].Bold = true;
                    textArea.Styles[ScintillaNET.Style.PhpScript.HStringVariable].ForeColor = Color.DarkOrange;
                    textArea.Styles[ScintillaNET.Style.PhpScript.HStringVariable].Weight = 700;
                    textArea.Styles[ScintillaNET.Style.PhpScript.Operator].ForeColor = Color.DarkGoldenrod;

                    textArea.SetKeywords(0, @"7z adduser alias apt-get ar as asa autoconf automake awk banner base64 basename bash bc bdiff blkid break bsdcpio bsdtar bunzip2 bzcmp bzdiff bzegrep bzfgrep bzgrep bzip2 bzip2recover bzless bzmore c++ cal calendar case cat cc cd cfdisk chattr chgrp chmod chown chroot chvt cksum clang clang++ clear cmp col column comm compgen compress continue convert cp cpio crontab crypt csplit ctags curl cut date dc dd deallocvt declare deluser depmod deroff df dialog diff diff3 dig dircmp dirname disown dmesg do done dpkg dpkg-deb du echo ed egrep elif else env esac eval ex exec exit expand export expr fakeroot false fc fdisk ffmpeg fgrep fi file find flex flock fmt fold for fsck function functions fuser fusermount g++ gas gawk gcc gdb genisoimage getconf getopt getopts git gpg gpgsplit gpgv grep gres groff groups gunzip gzexe hash hd head help hexdump hg history httpd iconv id if ifconfig ifdown ifquery ifup in insmod integer inxi ip jobs join kill killall killall5 lc ld ldd let lex line ln local logname look ls lsattr lsb_release lsblk lscpu lshw lslocks lsmod lsusb lzcmp lzegrep lzfgrep lzgrep lzless lzma lzmainfo lzmore m4 mail mailx make man mkdir mkfifo mkswap mktemp modinfo modprobe mogrify more mount msgfmt mt mv nameif nasm nc ndisasm netcat newgrp nl nm nohup ntps objdump od openssl p7zip pacman passwd paste patch pathchk pax pcat pcregrep pcretest perl pg ping ping6 pivot_root poweroff pr print printf ps pwd python python2 python3 ranlib read readlink readonly reboot reset return rev rm rmdir rmmod rpm rsync sed select service set sh sha1sum sha224sum sha256sum sha3sum sha512sum shift shred shuf shutdown size sleep sort spell split start stop strings strip stty su sudo sum suspend switch_root sync systemctl tac tail tar tee test then time times touch tr trap troff true tsort tty type typeset ulimit umask umount unalias uname uncompress unexpand uniq unlink unlzma unset until unzip unzipsfx useradd userdel uudecode uuencode vi vim wait wc wget whence which while who wpaste wstart xargs xdotool xxd xz xzcat xzcmp xzdiff xzfgrep xzgrep xzless xzmore yes yum zcat zcmp zdiff zegrep zfgrep zforce zgrep zless zmore znew zsh");
                    break;
            }
        }

        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private void InitNumberMargin(Scintilla TextArea)
        {
            var nums = TextArea.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            TextArea.MarginClick += TextArea_MarginClick;
        }

        private void InitBookmarkMargin(Scintilla textArea)
        {
            var margin = textArea.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = textArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(Helper.IntToColor(0xFF003B));
            marker.SetForeColor(Helper.IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding(Scintilla textArea)
        {

            //textArea.SetFoldMarginColor(true, Helper.IntToColor(BACK_COLOR));
            //textArea.SetFoldMarginHighlightColor(true, Helper.IntToColor(BACK_COLOR));

            // Enable code folding
            textArea.SetProperty("fold", "1");
            textArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            textArea.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            textArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            textArea.Margins[FOLDING_MARGIN].Sensitive = true;
            textArea.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                textArea.Markers[i].SetForeColor(Helper.IntToColor(BACK_COLOR)); // styles for [+] and [-]
                textArea.Markers[i].SetBackColor(Helper.IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            textArea.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            textArea.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            textArea.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            textArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            textArea.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            textArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            textArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            textArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
        {
            Scintilla TextArea = sender as Scintilla;
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        #endregion

    }
}
