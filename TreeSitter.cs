using System;
using System.Runtime.InteropServices;

namespace SharpSitter;

internal static class TreeSitterApi
{
  private const string _dll = "libtree-sitter";

  internal enum TSInputEncoding
  {
    TSInputEncodingUTF8,
    TSInputEncodingUTF16,
  }

  internal enum TSSymbolType
  {
    TSSymbolTypeRegular,
    TSymbolTypeAnonymous,
    TSymbolTypeAuxiliary,
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct TSPoint
  {
    internal uint row;
    internal uint column;
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct TSRange
  {
    internal TSPoint start_point;
    internal TSPoint end_point;
    internal uint start_byte;
    internal uint end_byte;
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct TSInput
  {
  }

  internal enum TSLogType
  {
    TSLogTypeParse,
    TSLogTypeLex,
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct TSLogger
  {
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct TSInputEdit
  {
    internal uint start_byte;
    internal uint old_end_byte;
    internal uint new_end_byte;
    TSPoint start_point;
    TSPoint old_end_point;
    TSPoint new_end_point;
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct TSNode
  {
    uint[] content;
    IntPtr id;
    IntPtr tree;
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct TSTreeCursor
  {
    IntPtr tree;
    IntPtr id;
    uint[] context;
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct TSQueryCapture
  {
    TSNode node;
    uint index;
  }

  internal enum TSQuantifier
  {
    TSQuantifierZero,
    TSQuantifierZeroOrOne,
    TSQuantifierZeroOrMore,
    TSQuantifierOne,
    TSQuantifierOneOrMore,
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct TSQueryMatch
  {
    uint id;
    ushort pattern_index;
    ushort capture_count;
    TSQueryCapture[] captures;
  }

  internal enum TSQueryPredicateStepType
  {
    TSQueryPredicateStepTypeDone,
    TSQueryPredicateStepTypeCapture,
    TSQueryPredicateStepTypeString,
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct TSQueryPredicateStep
  {
    TSQueryPredicateStepType type;
    uint value_id;
  }

  internal enum TSQueryError
  {
    TSQueryErrorNone,
    TSQueryErrorSyntax,
    TSQueryErrorNodeType,
    TSQueryErrorField,
    TSQueryErrorCapture,
    TSQueryErrorStructure,
    TSQueryErrorLanguage,
  }

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_parser_new();

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_parser_delete(IntPtr parser);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_parser_set_language(IntPtr self, IntPtr language);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_parser_language(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_parser_set_included_ranges(
      IntPtr self,
      TSRange[] ranges,
      uint length);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSRange[] ts_parser_included_ranges(
    IntPtr self,
    out uint length);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_parser_parse(
    IntPtr self,
    IntPtr old_tree,
    IntPtr input);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_parser_parse_string(
    IntPtr self,
    IntPtr old_tree,
    string @string,
    uint length);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_parser_parse_string_encoding(
    IntPtr self,
    IntPtr old_tree,
    string @string,
    uint length,
    TSInputEncoding encoding);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_parser_reset(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_parser_set_timeout_micros(IntPtr self, ulong timeout);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern ulong ts_parser_timeout_micros(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_parser_set_cancellation_flag(IntPtr self, IntPtr flag);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_parser_cancellation_flag(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_parser_set_logger(IntPtr self, TSLogger logger);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSLogger ts_parser_logger(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_parser_print_dot_graphs(IntPtr self, int file);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_tree_copy(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_tree_delete(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_tree_root_node(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_tree_root_node_with_offset(
      IntPtr self,
      uint offset_bytes,
      TSPoint offset_point);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_tree_language(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_tree_edit(IntPtr self, IntPtr edit);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_tree_get_changed_ranges(
      IntPtr old_tree,
      IntPtr new_tree,
      out uint length);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_tree_print_dot_graph(IntPtr self, int file);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern string ts_node_type(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_node_start_byte(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSPoint ts_node_start_point(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_node_end_byte(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSPoint ts_node_end_point(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern string ts_node_string(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_node_is_null(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_node_is_named(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_node_is_missing(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_node_is_extra(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_node_has_changes(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_node_has_error(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_child(TSNode node, uint child_index);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_field_name_for_child(TSNode node, uint child_index);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_node_child_count(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_named_child(TSNode node, uint child_index);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_node_named_child_count(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_child_by_field_name(
      TSNode node,
      string field_name,
      uint field_name_length);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_child_by_field_id(TSNode node, uint field_id);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_next_sibling(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_prev_sibling(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_next_named_sibling(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_prev_named_sibling(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_first_child_for_byte(TSNode node, uint byte_offset);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_first_named_child_for_byte(TSNode node, uint byte_offset);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_descendant_for_byte_range(
      TSNode node,
      uint start_byte,
      uint end_byte);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_descendant_for_point_range(
      TSNode node,
      TSPoint start_point,
      TSPoint end_point);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_named_descendant_for_byte_range(
      TSNode node,
      uint start_byte,
      uint end_byte);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_node_named_descendant_for_point_range(
      TSNode node,
      TSPoint start_point,
      TSPoint end_point);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_node_edit(IntPtr node, IntPtr edit);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_node_eq(TSNode self, TSNode other);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSTreeCursor ts_tree_cursor_new(TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_tree_cursor_delete(IntPtr cursor);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_tree_cursor_reset(IntPtr cursor, TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSNode ts_tree_cursor_current_node(IntPtr cursor);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern string ts_tree_cursor_current_field_name(IntPtr cursor);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_tree_cursor_current_field_id(IntPtr cursor);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_tree_cursor_goto_parent(IntPtr cursor);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_tree_cursor_goto_next_sibling(IntPtr cursor);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_tree_cursor_goto_first_child(IntPtr cursor);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern long ts_tree_cursor_goto_first_child_for_byte(
      IntPtr cursor,
      uint byte_offset);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern long ts_tree_cursor_goto_first_child_for_point(
      IntPtr cursor,
      TSPoint point);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSTreeCursor ts_tree_cursor_copy(IntPtr cursor);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_query_new(
      IntPtr language,
      string source,
      uint source_len,
      uint error_offset,
      IntPtr error_type);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_query_delete(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_query_pattern_count(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_query_capture_count(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_query_string_count(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_query_start_byte_for_pattern(IntPtr self, uint pattern_index);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_query_predicates_for_pattern(
      IntPtr self,
      uint pattern_index,
      out uint length);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_query_is_pattern_rooted(
      IntPtr self,
      uint pattern_index);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_query_is_pattern_guaranteed_at_step(
      IntPtr self,
      uint byte_offset);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern string ts_query_capture_name_for_id(
      IntPtr self,
      uint id,
      out uint length);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSQuantifier ts_query_capture_quantifier_for_id(
      IntPtr self,
      uint pattern_id,
      uint capture_id);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern string ts_query_string_value_for_id(
      IntPtr self,
      uint id,
      out uint length);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_query_disable_capture(
      IntPtr self,
      string capture,
      uint length);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_query_disable_pattern(
      IntPtr self,
      uint pattern_index);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern IntPtr ts_query_cursor_new();

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_query_cursor_delete(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_query_cursor_exec(
      IntPtr self,
      IntPtr query,
      TSNode node);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_query_cursor_did_exceed_match_limit(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_query_cursor_match_limit(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_query_cursor_set_match_limit(IntPtr self, uint limit);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_query_cursor_set_byte_range(
      IntPtr self,
      uint start_byte,
      uint end_byte);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_query_set_point_range(
      IntPtr self,
      TSPoint start_point,
      TSPoint end_point);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_query_cursor_next_match(IntPtr self, IntPtr match);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern void ts_query_cursor_remove_match(IntPtr self, uint id);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern bool ts_query_cursor_next_capture(
      IntPtr self,
      IntPtr match,
      out uint capture_index);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_language_symbol_count(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern string ts_language_symbol_name(IntPtr self, uint symbol);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_language_symbol_for_name(
      IntPtr self,
      string name,
      uint length,
      bool is_named);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_language_field_count(IntPtr self);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern string ts_language_field_name_for_id(
      IntPtr self,
      uint field_id);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_language_field_id_for_name(
      IntPtr self,
      string name,
      uint length);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern TSSymbolType ts_language_symbol_type(IntPtr self, uint symbol);

  [DllImport(_dll, CallingConvention = CallingConvention.Cdecl)]
  internal static extern uint ts_language_version(IntPtr self);
}
